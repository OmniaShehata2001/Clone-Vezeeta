using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.ServicesRepositories;
using Vezeeta.Dtos.DTOS.ServicesDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.ServicesServices
{
    public class SubServicesReviewsServices : ISubServicesReviewsServices
    {
        private readonly ISubServicesReviewsRepository _subServicesReviewsRepository;
        private readonly IMapper _mapper;

        public SubServicesReviewsServices(ISubServicesReviewsRepository subServicesReviewsRepository, IMapper mapper)
        {
            _subServicesReviewsRepository = subServicesReviewsRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<SubServicesReviewsDto>> Create(SubServicesReviewsDto subServicesReviews)
        {
            var Review = (await _subServicesReviewsRepository.GetAllasync()).Where(s => s.SubServiceId ==  subServicesReviews.SubServiceId && s.UserId == subServicesReviews.UserId);
            if (Review == null)
            {
                return new ResultView<SubServicesReviewsDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Review Added Before"
                };
            }
            var NewReview = await _subServicesReviewsRepository.Createasync(_mapper.Map<SubServiceReview>(subServicesReviews));
            await _subServicesReviewsRepository.SaveAsync();

            return new ResultView<SubServicesReviewsDto>
            {
                Entity = _mapper.Map<SubServicesReviewsDto>(NewReview),
                IsSuccess = true,
                Message = " The Review Created Successfully"
            };
        }

        public async Task<ResultView<SubServicesReviewsDto>> Delete(int id)
        {
            var Review = await _subServicesReviewsRepository.GetOneasync(id);
            if (Review == null)
            {
                return new ResultView<SubServicesReviewsDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Review Not Found"
                };
            }

            var DeletedReview = await _subServicesReviewsRepository.Deleteasync(Review);
            await _subServicesReviewsRepository.SaveAsync();

            return new ResultView<SubServicesReviewsDto>
            {
                Entity = _mapper.Map<SubServicesReviewsDto>(DeletedReview),
                IsSuccess = true,
                Message = " The Review Exist"
            };
        }

        public async Task<ResultDataList<SubServicesReviewsDto>> GetAll(int pagenumber, int Items)
        {
            var Reviews = (await _subServicesReviewsRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            var ReviewsDto = Reviews.Skip(Items * (pagenumber - 1)).Take(Items).Select(s => new SubServicesReviewsDto
            {
                Comment = s.Comment,
                Rating = s.Rating,
                SubServiceId = s.SubServiceId,
                UserId = s.UserId,
                Id = s.Id
            });

            if(Reviews is null)
            {
                return new ResultDataList<SubServicesReviewsDto>
                {
                    Entities = null,
                    Count = 0,
                };
            }
            return new ResultDataList<SubServicesReviewsDto>
            {
                Entities = ReviewsDto.ToList(),
                Count = Reviews.Count()
            };
        }

        public async Task<ResultView<SubServicesReviewsDto>> GetOne(int id)
        {
            var Review = await _subServicesReviewsRepository.GetOneasync(id);
            if(Review is null)
            {
                return new ResultView<SubServicesReviewsDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "The Review Not Found"
                };
            }
            return new ResultView<SubServicesReviewsDto>
            {
                Entity = _mapper.Map<SubServicesReviewsDto>(Review),
                IsSuccess = true,
                Message = "The Review Exist"
            };
        }

        public async Task<ResultDataList<SubServicesReviewsDto>> GetReviewBysubServiceId(int subServiceId , int pageNumber , int Items)
        {
            var Reviews = (await _subServicesReviewsRepository.GetAllasync()).Where(s => s.SubServiceId == subServiceId && s.IsDeleted == false);
            var ReviewsDto = Reviews.Skip(Items * (pageNumber - 1)).Take(Items).Select(s => new SubServicesReviewsDto
            {
                Comment = s.Comment,
                Rating = s.Rating,
                SubServiceId = subServiceId,
                UserId = s.UserId,
                Id = s.Id
            });
            if(Reviews == null)
            {
                return new ResultDataList<SubServicesReviewsDto>
                {
                    Entities = null,
                    Count = 0
                };
            }
            return new ResultDataList<SubServicesReviewsDto>
            {
                Entities = ReviewsDto.ToList(),
                Count = Reviews.Count()
            };
        }

        public async Task<ResultView<SubServicesReviewsDto>> Update(SubServicesReviewsDto subServicesReviews)
        {
            var UpdatedReview = await _subServicesReviewsRepository.Updateasync(_mapper.Map<SubServiceReview>(subServicesReviews));
            await _subServicesReviewsRepository.SaveAsync();
            return new ResultView<SubServicesReviewsDto>
            {
                Entity = _mapper.Map<SubServicesReviewsDto>(subServicesReviews),
                IsSuccess = true,
                Message = " The Review Updated Successfully"
            };
        }
    }
}
