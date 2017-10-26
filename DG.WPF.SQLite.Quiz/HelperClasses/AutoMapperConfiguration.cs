using AutoMapper;
using DG.WPF.SQLite.Model;
using DG.WPF.SQLite.Quiz.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Quiz.HelperClasses
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
                {
                    cfg.AddProfile(new CategoryProfile());
                    cfg.AddProfile(new SubCategoryProfile());
                    cfg.AddProfile(new AdditionalInfoProfile());
                    cfg.AddProfile(new QuestionProfile());
                    cfg.AddProfile(new AnswerProfile());
                    cfg.AddProfile(new QuizSessionProfile());
                    cfg.AddProfile(new QuizSessionQuestionProfile());
                    //cfg.AddProfile(new QuestionTypeProfile());
                });
        }
    }

    public class CategoryProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Category, CategoryViewModel>();
            Mapper.CreateMap<CategoryViewModel, Category>();
        }
    }

    public class SubCategoryProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<SubCategory, SubCategoryViewModel>();
            Mapper.CreateMap<SubCategoryViewModel, SubCategory>();
        }
    }

    public class AdditionalInfoProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<AdditionalInfo, AdditionalInfoViewModel>();
            Mapper.CreateMap<AdditionalInfoViewModel, AdditionalInfo>();
        }
    }

    public class QuestionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Question, QuizSessionQuestionViewModel>();
            Mapper.CreateMap<QuizSessionQuestionViewModel, Question>();
        }
    }

    public class AnswerProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Answer, AnswerViewModel>();
            Mapper.CreateMap<AnswerViewModel, Answer>();
        }
    }

    public class QuizSessionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<QuizSession, QuizSessionViewModel>();
            Mapper.CreateMap<QuizSessionViewModel, QuizSession>();
        }
    }

    public class QuizSessionQuestionProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<QuizSessionQuestion, QuizSessionQuestionViewModel>()
                .ForMember(q => q.Id, m => m.MapFrom(
                    s => s.QuestionId));
            Mapper.CreateMap<QuizSessionQuestionViewModel, QuizSessionQuestion>()
                .ForMember(q => q.QuestionId, m => m.MapFrom(
                    s => s.Id));
        }
    }
    //public class QuestionTypeProfile : Profile
    //{
    //    protected override void Configure()
    //    {
    //        Mapper.CreateMap<QuestionType, QuestionTypeViewModel>();
    //        Mapper.CreateMap<QuestionTypeViewModel, QuestionType>();
    //    }
    //}

}