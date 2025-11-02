global using System.Linq.Expressions;

global using TourApp.Domain.Entities;
global using TourApp.Persistence.TypeConfigs.ValueComparers;
global using TourApp.Persistence.TypeConfigs.ValueConverters;
global using TourApp.Persistence.DbContext;
global using TourApp.Persistence.Extensions;
global using TourApp.Application.Specifications;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using TourApp.Application.Contracts.Repositories;
global using TourApp.Application.Features.Tours.Contracts.Repositories;
global using TourApp.Application.Features.Users.Contracts.Repositories;
global using TourApp.Domain.Entities.Booking;
global using TourApp.Domain.Entities.Tour;
global using TourApp.Domain.Entities.Tour.Common;
global using TourApp.Domain.Entities.User;
global using TourApp.Domain.Entities.User.Common;
global using TourApp.Persistence.Repositories;