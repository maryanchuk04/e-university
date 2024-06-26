﻿using EUniversity.Shared.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EUniversity.Shared.Swagger;

public class SharedApiKeyHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= [];

        // PreShared key parameter
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = SharedApiKeyContants.HeaderName,
            In = ParameterLocation.Header,
            Required = true,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}