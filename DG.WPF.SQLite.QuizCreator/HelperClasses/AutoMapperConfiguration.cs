using AutoMapper;
using DG.WPF.SQLite.Model;
using DG.WPF.SQLite.QuizCreator.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.QuizCreator.HelperClasses
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
            Mapper.CreateMap<Question, QuestionViewModel>();
            Mapper.CreateMap<QuestionViewModel, Question>();
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

    //public class QuestionTypeProfile : Profile
    //{
    //    protected override void Configure()
    //    {
    //        Mapper.CreateMap<QuestionType, QuestionTypeViewModel>();
    //        Mapper.CreateMap<QuestionTypeViewModel, QuestionType>();
    //    }
    //}

}