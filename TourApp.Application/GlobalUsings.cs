global using TourApp.Application.Utilities;
global using TourApp.Application.Specifications;
global using TourApp.Application.Features.Tours.Specifications;
global using TourApp.Application.Features.Tours.Specifications.Common;

global using System.Linq.Expressions;

global using Microsoft.Extensions.DependencyInjection;

global using MediatR;
global using AutoMapper;
global using FluentValidation;
global using FluentValidation.Results;
global using TourApp.Application.Contracts.Repositories;
global using TourApp.Application.Contracts.Services.Account;
global using TourApp.Application.Extensions;
global using TourApp.Application.Features.Tours.Contracts.Repositories;
global using TourApp.Application.Features.Tours.Queries;
global using TourApp.Application.Features.Users.Contracts.Repositories;
global using TourApp.Application.Features.Users.Dto;
global using TourApp.Application.Features.Users.Specifications;
global using TourApp.Application.Models.Authentication;
global using TourApp.Application.Models.Email;
global using TourApp.Application.Models.Registration;
global using TourApp.Application.Models.Result;
global using TourApp.Domain.Entities.Tour;
global using TourApp.Domain.Entities.User;
global using TourApp.Domain.Entities.User.Common;