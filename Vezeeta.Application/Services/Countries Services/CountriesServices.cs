using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.CountriesRepositries;
using Vezeeta.Dtos.DTOS.CountryDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Infrastucture.CountriesRepos;
using Vezeeta.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Vezeeta.Application.Services.Countries_Services
{
    public class CountriesServices : ICountriesServices
    {
        private readonly ICountriesRepository _countriesRepository;
        private readonly ICountryImagesRepository _countryImagesRepository;
        private readonly IMapper _mapper;

        public CountriesServices(ICountriesRepository countriesRepository,IMapper mapper , ICountryImagesRepository countryImagesRepository ) 
        {
            _countriesRepository = countriesRepository;
            _countryImagesRepository = countryImagesRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<CreateOrUpdateCountryDto>> CreateAsync(CreateOrUpdateCountryDto Countrydto)
        {
            var countries = (await _countriesRepository.GetAllasync()).FirstOrDefault(s => s.Name == Countrydto.Name);
            if(countries is null)
            {
                var CountryModel = _mapper.Map<Countries>(Countrydto);

                using var datastream = new MemoryStream();
                await Countrydto.FlagImage.CopyToAsync(datastream);
                var ImgToByts = datastream.ToArray();
                string ImgToBytsString = Convert.ToBase64String(ImgToByts);
                CountryModel.FlagImage = ImgToBytsString;

                var newCountry = await _countriesRepository.Createasync(CountryModel);
                await _countriesRepository.SaveAsync();

                var imgPaths = new List<string>();
                foreach (var image in Countrydto.CountryImages)
                {
                    using var datastream2 = new MemoryStream();
                    await image.CopyToAsync(datastream2);
                    var Img2Byts = datastream2.ToArray();
                    string img2Base64String = Convert.ToBase64String(Img2Byts);
                    imgPaths.Add(img2Base64String);
                }

                foreach (var image in imgPaths)
                {
                    var imgDto = new CountryImagesDto { ImgPath = image, CountryId = newCountry.Id };
                    var countryimagemodel = _mapper.Map<CountriesImages>(imgDto);
                    var ImgesCreated = await _countryImagesRepository.Createasync(countryimagemodel);
                    await _countryImagesRepository.SaveAsync();
                }

                return new ResultView<CreateOrUpdateCountryDto>
                {
                    Entity = _mapper.Map<CreateOrUpdateCountryDto>(newCountry),
                    IsSuccess = true,
                    Message = " The Country Created Successfully"
                };

            }
            else
            {
                return new ResultView<CreateOrUpdateCountryDto>
                {
                    Entity = null,
                    Message = "The Country Already Exist",
                    IsSuccess = false
                };
            }
        }

        public async Task<ResultView<CreateOrUpdateCountryDto>> DeleteAsync(int CountryId)
        {
            var CountryExist = await _countriesRepository.GetOneasync(CountryId);
            if(CountryExist is null)
            {
                return new ResultView<CreateOrUpdateCountryDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Country doesnot Exist"
                };
            }
            CountryExist.IsDeleted = true;
            await _countriesRepository.SaveAsync();

            var Images = await _countryImagesRepository.GetCountriesImages(CountryId);
            foreach(var image in Images)
            {
                image.IsDeleted = true;
            }
            await _countryImagesRepository.SaveAsync();
            return new ResultView<CreateOrUpdateCountryDto>
            {
                Entity = _mapper.Map<CreateOrUpdateCountryDto>(CountryExist),
                IsSuccess = true,
                Message = "The Country is Deleted"
            };
             
        }

        public async Task<ResultDataList<CountriesImagesDTos>> GetAll()
        {
            var Countries =(await _countriesRepository.GetAllasync()).Where(s => s.IsDeleted != true).ToList();
            var CountriesDto = _mapper.Map<List<CountriesImagesDTos>>(Countries);
            foreach (var country in CountriesDto)
            {
                var images = await _countryImagesRepository.GetCountriesImages(country.Id);
                country.Images = _mapper.Map<List<CountryImagesDto>>(images);
                
            }


            return new ResultDataList<CountriesImagesDTos>
            {
                Entities = CountriesDto,
                Count = Countries.Count()
            };
        }

        public async Task<ResultView<CountriesImagesDTos>> GetOne(int CountryId)
        {
            var Country = await _countriesRepository.GetOneasync(CountryId);
            var Images = await _countryImagesRepository.GetCountriesImages(CountryId);
            Country.CountriesImages = Images.ToList();
            var CountriesDto = _mapper.Map<CountriesImagesDTos>(Country);
            CountriesDto.Images = _mapper.Map<List<CountryImagesDto>>(Country.CountriesImages);
            return new ResultView<CountriesImagesDTos>
            {
                Entity = CountriesDto,
                IsSuccess = true,
                Message = "The Country Exist"
            };
        }

        public async Task<ResultView<CountriesImagesDTos>> UpdateAsync(CreateOrUpdateCountryDto Countrydto)
        {
            var CountryModel = _mapper.Map<Countries>(Countrydto);

            using var datastream = new MemoryStream();
            await Countrydto.FlagImage.CopyToAsync(datastream);
            var ImgToByts = datastream.ToArray();
            string ImgToBytsString = Convert.ToBase64String(ImgToByts);
            CountryModel.FlagImage = ImgToBytsString;

            var UpdatedCountry = await _countriesRepository.Updateasync(CountryModel);
            await _countriesRepository.SaveAsync();

            if(Countrydto.CountryImages is not null)
            {
                var Images = await _countryImagesRepository.GetCountriesImages(UpdatedCountry.Id);
                foreach (var image in Images)
                {
                    var DeletedImage = await _countryImagesRepository.Deleteasync(image);
                }
                await _countryImagesRepository.SaveAsync();

                var imgPaths = new List<string>();
                foreach (var image in Countrydto.CountryImages)
                {
                    using var datastream2 = new MemoryStream();
                    await image.CopyToAsync(datastream2);
                    var Img2Byts = datastream2.ToArray();
                    string img2Base64String = Convert.ToBase64String(Img2Byts);
                    imgPaths.Add(img2Base64String);
                }

                foreach (var image in imgPaths)
                {
                    var imgDto = new CountryImagesDto { ImgPath = image, CountryId = UpdatedCountry.Id };
                    var countryimagemodel = _mapper.Map<CountriesImages>(imgDto);
                    var ImgesCreated = await _countryImagesRepository.Createasync(countryimagemodel);
                    await _countryImagesRepository.SaveAsync();
                    UpdatedCountry.CountriesImages.Add(ImgesCreated);
                }
            }
            //var Images = await _countryImagesRepository.GetCountriesImages(UpdatedCountry.Id);
            //foreach (var image in Images)
            //{
            //    var DeletedImage = await _countryImagesRepository.Deleteasync(image);
            //}
            //await _countryImagesRepository.SaveAsync();

            //var imgPaths = new List<string>();
            //foreach (var image in Countrydto.CountryImages)
            //{
            //    using var datastream2 = new MemoryStream();
            //    await image.CopyToAsync(datastream2);
            //    var Img2Byts = datastream2.ToArray();
            //    string img2Base64String = Convert.ToBase64String(Img2Byts);
            //    imgPaths.Add(img2Base64String);
            //}

            //foreach (var image in imgPaths)
            //{
            //    var imgDto = new CountryImagesDto { ImgPath = image, CountryId = UpdatedCountry.Id };
            //    var countryimagemodel = _mapper.Map<CountriesImages>(imgDto);
            //    var ImgesCreated = await _countryImagesRepository.Createasync(countryimagemodel);
            //    await _countryImagesRepository.SaveAsync();
            //    UpdatedCountry.CountriesImages.Add(ImgesCreated);
            //}

            var UpdatedCountryDto = _mapper.Map<CountriesImagesDTos>(UpdatedCountry);
            UpdatedCountryDto.Images = _mapper.Map<List<CountryImagesDto>>(UpdatedCountry.CountriesImages);

            return new ResultView<CountriesImagesDTos>
            {
                Entity = UpdatedCountryDto,
                IsSuccess = true,
                Message = " The Country Updated Successfully"
            };
        }
    }
}
