using FluentValidation;
using SimpleBlog.Dto.Dto;
using SimpleBlog.Dto.Notifications;
using SimpleBlog.Dto.Validators;
using SimpleBlog.Repository.Interfaces;
using SimpleBlog.Repository.Repositories;
using SimpleBlog.Service.Interfaces;
using SimpleBlog.Service.Services;

namespace SimpleBlog.API.Configuration.Ioc
{
    public class DependencyInjection
    {
        public static void DependencyInjectionRepositories(ref WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        }

        public static void DependencyInjectionServices(ref WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IBlogPostService, BlogPostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
        }

        public static void DependencyInjectionValidations(ref WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<BlogPostDto>, BlogPostValidator>();
            builder.Services.AddScoped<IValidator<CommentDto>, CommentValidator>();
        }

        public static void DependencyInjectionNotifier(ref WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<INotifier, Notifier>();
        }
    }
}
