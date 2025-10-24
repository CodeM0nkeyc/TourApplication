// Global using directives

global using System.Security.Cryptography;
global using System.Text;
global using AutoMapper;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;
global using TourApp.Application.Contracts.Services.Account;
global using TourApp.Application.Contracts.Services.Email;
global using TourApp.Application.Features.Users.Commands;
global using TourApp.Application.Features.Users.Contracts.Repositories;
global using TourApp.Application.Features.Users.Contracts.Services;
global using TourApp.Application.Features.Users.Specifications;
global using TourApp.Application.Models.Authentication;
global using TourApp.Application.Models.Email;
global using TourApp.Application.Specifications;
global using TourApp.Domain.Entities;
global using TourApp.Domain.Entities.User;
global using TourApp.Infrastructure.Services.Security.Generator.Contracts;
global using TourApp.Infrastructure.Services.Security.Hashing;
global using TourApp.Infrastructure.Services.Security.Hashing.Contracts;