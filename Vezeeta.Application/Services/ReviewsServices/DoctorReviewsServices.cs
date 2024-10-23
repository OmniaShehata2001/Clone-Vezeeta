using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vezeeta.Application.Contract.ReviewsRepositories;
using Vezeeta.Dtos.DTOS.ReviewsDtos;
using Vezeeta.Dtos.Result;
using Vezeeta.Models;

namespace Vezeeta.Application.Services.ReviewsServices
{
    public class DoctorReviewsServices : IDoctorReviewesServices
    {
        private readonly IDoctorReviewsRepository _doctorReviewsRepository;
        private readonly IMapper _mapper;

        public DoctorReviewsServices(IDoctorReviewsRepository doctorReviewsRepository, IMapper mapper) 
        {
            _doctorReviewsRepository = doctorReviewsRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<DoctorReviewDto>> Create(DoctorReviewDto doctorReviewDto)
        {
            var DoctorReviewExist = (await _doctorReviewsRepository.GetAllasync())
                .FirstOrDefault(s=> s.Id == doctorReviewDto.Id && s.UserId == doctorReviewDto.UserId);
            if (DoctorReviewExist is null) 
            {
                var CreatedReview = await _doctorReviewsRepository.Createasync(_mapper.Map<DoctorReviews>(doctorReviewDto));
                await _doctorReviewsRepository.SaveAsync();
                return new ResultView<DoctorReviewDto>()
                {
                    Entity = _mapper.Map<DoctorReviewDto>(CreatedReview),
                    IsSuccess = true,
                    Message = "The Review Created Successfully"
                };
            }
            return new ResultView<DoctorReviewDto>()
            {
                Entity = null,
                IsSuccess = false,
                Message = "Not Allowed more than one Review"
            };
        }

        public async Task<ResultView<DoctorReviewDto>> Delete(int doctorReviewId)
        {
            var ReviewExist = await _doctorReviewsRepository.GetOneasync(doctorReviewId);
            if(ReviewExist is null)
            {
                return new ResultView<DoctorReviewDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Review Not Found"
                };
            }
            ReviewExist.IsDeleted = true;
            await _doctorReviewsRepository.SaveAsync();
            return new ResultView<DoctorReviewDto>()
            {
                Entity = _mapper.Map<DoctorReviewDto>(ReviewExist),
                IsSuccess = true,
                Message = " The Review Deleted Successfully"
            };
        }

        public async Task<ResultDataList<DoctorReviewDto>> GetAll(int pageNumber , int Items)
        {
            var Reviews = (await _doctorReviewsRepository.GetAllasync()).Where(s => s.IsDeleted == false);
            var ReviewsDto = Reviews.Skip(Items * (pageNumber - 1)).Take(Items).Select(s => new DoctorReviewDto
            {
                Comment = s.Comment,
                Id = s.Id,
                Rating = s.Rating,
                DoctorId = s.DoctorId,
                UserId = s.UserId,
            }).ToList();
            return new ResultDataList<DoctorReviewDto>()
            {
                Entities = ReviewsDto,
                Count = Reviews.Count()
            };

        }

        public async Task<ResultDataList<DoctorReviewDto>> GetByDoctorId(int doctorReviewId, int pageNumber, int Items)
        {
            var Reviews = await _doctorReviewsRepository.GetReviewsByDrId(doctorReviewId);
            var ReviewsDto = Reviews.Skip(Items * (pageNumber - 1)).Take(Items).Select(s => new DoctorReviewDto
            {
                Comment = s.Comment,
                Id = s.Id,
                Rating = s.Rating,
                DoctorId = s.DoctorId,
                UserId = s.UserId
            }).ToList();
            if (Reviews is null)
            {
                return new ResultDataList<DoctorReviewDto>()
                {
                    Entities = null,
                    Count = 0
                };
            }
            return new ResultDataList<DoctorReviewDto>
            {
                Entities = ReviewsDto,
                Count = Reviews.Count()
            };
        }

        public async Task<ResultView<DoctorReviewDto>> GetOne(int doctorReviewId)
        {
            var Review = await _doctorReviewsRepository.GetOneasync(doctorReviewId);
            if(Review == null)
            {
                return new ResultView<DoctorReviewDto>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " The Review Not Found"
                };
            }
            return new ResultView<DoctorReviewDto>()
            {
                Entity = _mapper.Map<DoctorReviewDto>(Review),
                IsSuccess = true,
                Message = " The Review Already Exist"
            };
        }

        public async Task<ResultView<DoctorReviewDto>> Update(DoctorReviewDto doctorReviewDto)
        {
            var UpdatedReview = await _doctorReviewsRepository.Updateasync(_mapper.Map<DoctorReviews>(doctorReviewDto));
            await _doctorReviewsRepository.SaveAsync();
            return new ResultView<DoctorReviewDto>()
            {
                Entity = _mapper.Map<DoctorReviewDto>(UpdatedReview),
                IsSuccess = true,
                Message = "The Review Updated Successfully"
            };
        }
    }
}
