using Microsoft.AspNetCore.Mvc;
using RecordManagement.Api.Models;
using RecordManagement.Api.Services;
using RecordManagement.Api.DTOs;

namespace RecordManagement.Api.Endpoints;

public static class RecordEndpoints
{
    public static void MapRecordEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/records")
            .WithTags("Records")
            .WithOpenApi();

        // GET /api/records
        group.MapGet("/", async (IRecordService recordService) =>
        {
            var records = await recordService.GetAllRecordsAsync();
            return Results.Ok(records);
        })
        .WithName("GetAllRecords")
        .WithSummary("Get all records")
        .WithDescription("Retrieves all records from the system");

        // GET /api/records/{id}
        group.MapGet("/{id:int}", async (int id, IRecordService recordService) =>
        {
            var record = await recordService.GetRecordByIdAsync(id);
            return record is not null ? Results.Ok(record) : Results.NotFound();
        })
        .WithName("GetRecordById")
        .WithSummary("Get record by ID")
        .WithDescription("Retrieves a specific record by its ID");

        // POST /api/records
        group.MapPost("/", async ([FromBody] CreateRecordRequest request, IRecordService recordService) =>
        {
            var record = new Record
            {
                Date = request.Date,
                Name = request.Name,
                Alpha = request.Alpha,
                Beta = request.Beta,
                Gamma = request.Gamma,
                Delta = request.Delta,
                Milestone1StartDate = request.Milestone1StartDate,
                Milestone1CompletionDate = request.Milestone1CompletionDate,
                Milestone2StartDate = request.Milestone2StartDate,
                Milestone2CompletionDate = request.Milestone2CompletionDate,
                ClientName = request.ClientName,
                AgentName = request.AgentName
            };

            var createdRecord = await recordService.CreateRecordAsync(record);
            return Results.Created($"/api/records/{createdRecord.Id}", createdRecord);
        })
        .WithName("CreateRecord")
        .WithSummary("Create a new record")
        .WithDescription("Creates a new record in the system");

        // PUT /api/records/{id}
        group.MapPut("/{id:int}", async (int id, [FromBody] UpdateRecordRequest request, IRecordService recordService) =>
        {
            if (id != request.Id)
                return Results.BadRequest("ID mismatch");

            var record = new Record
            {
                Id = request.Id,
                Date = request.Date,
                Name = request.Name,
                Alpha = request.Alpha,
                Beta = request.Beta,
                Gamma = request.Gamma,
                Delta = request.Delta,
                Milestone1StartDate = request.Milestone1StartDate,
                Milestone1CompletionDate = request.Milestone1CompletionDate,
                Milestone2StartDate = request.Milestone2StartDate,
                Milestone2CompletionDate = request.Milestone2CompletionDate,
                ClientName = request.ClientName,
                AgentName = request.AgentName
            };

            var updatedRecord = await recordService.UpdateRecordAsync(record);
            return updatedRecord is not null ? Results.Ok(updatedRecord) : Results.NotFound();
        })
        .WithName("UpdateRecord")
        .WithSummary("Update an existing record")
        .WithDescription("Updates an existing record in the system");

        // DELETE /api/records/{id}
        group.MapDelete("/{id:int}", async (int id, IRecordService recordService) =>
        {
            var deleted = await recordService.DeleteRecordAsync(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteRecord")
        .WithSummary("Delete a record")
        .WithDescription("Deletes a record from the system");
    }
}
