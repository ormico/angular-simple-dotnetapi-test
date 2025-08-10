using RecordManagement.Api.Models;

namespace RecordManagement.Api.Services;

public interface IRecordService
{
    Task<IEnumerable<Record>> GetAllRecordsAsync();
    Task<Record?> GetRecordByIdAsync(int id);
    Task<Record> CreateRecordAsync(Record record);
    Task<Record?> UpdateRecordAsync(Record record);
    Task<bool> DeleteRecordAsync(int id);
}

public class InMemoryRecordService : IRecordService
{
    private readonly List<Record> _records = new();
    private int _nextId = 1;

    public InMemoryRecordService()
    {
        // Seed with sample data matching the CSV structure
        _records.AddRange(new[]
        {
            new Record 
            { 
                Id = _nextId++, 
                Date = DateTime.UtcNow.AddDays(-5),
                Name = "Project Alpha", 
                Alpha = 125.50m,
                Beta = 87.25m,
                Gamma = 203.75m,
                Delta = 156.00m,
                Milestone1StartDate = DateTime.UtcNow.AddDays(-30),
                Milestone1CompletionDate = DateTime.UtcNow.AddDays(-20),
                Milestone2StartDate = DateTime.UtcNow.AddDays(-15),
                Milestone2CompletionDate = null,
                ClientName = "Acme Corporation",
                AgentName = "John Smith"
            },
            new Record 
            { 
                Id = _nextId++, 
                Date = DateTime.UtcNow.AddDays(-3),
                Name = "Project Beta", 
                Alpha = 98.75m,
                Beta = 145.30m,
                Gamma = 78.90m,
                Delta = 234.15m,
                Milestone1StartDate = DateTime.UtcNow.AddDays(-25),
                Milestone1CompletionDate = DateTime.UtcNow.AddDays(-18),
                Milestone2StartDate = DateTime.UtcNow.AddDays(-10),
                Milestone2CompletionDate = DateTime.UtcNow.AddDays(-2),
                ClientName = "TechCorp Industries",
                AgentName = "Sarah Johnson"
            },
            new Record 
            { 
                Id = _nextId++, 
                Date = DateTime.UtcNow.AddDays(-10),
                Name = "Project Gamma", 
                Alpha = 67.25m,
                Beta = 189.50m,
                Gamma = 123.75m,
                Delta = 87.60m,
                Milestone1StartDate = DateTime.UtcNow.AddDays(-40),
                Milestone1CompletionDate = DateTime.UtcNow.AddDays(-35),
                Milestone2StartDate = null,
                Milestone2CompletionDate = null,
                ClientName = "Global Solutions Ltd",
                AgentName = "Mike Davis"
            }
        });
    }

    public Task<IEnumerable<Record>> GetAllRecordsAsync()
    {
        return Task.FromResult(_records.AsEnumerable());
    }

    public Task<Record?> GetRecordByIdAsync(int id)
    {
        var record = _records.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(record);
    }

    public Task<Record> CreateRecordAsync(Record record)
    {
        record.Id = _nextId++;
        _records.Add(record);
        return Task.FromResult(record);
    }

    public Task<Record?> UpdateRecordAsync(Record record)
    {
        var existingRecord = _records.FirstOrDefault(r => r.Id == record.Id);
        if (existingRecord == null)
            return Task.FromResult<Record?>(null);

        existingRecord.Date = record.Date;
        existingRecord.Name = record.Name;
        existingRecord.Alpha = record.Alpha;
        existingRecord.Beta = record.Beta;
        existingRecord.Gamma = record.Gamma;
        existingRecord.Delta = record.Delta;
        existingRecord.Milestone1StartDate = record.Milestone1StartDate;
        existingRecord.Milestone1CompletionDate = record.Milestone1CompletionDate;
        existingRecord.Milestone2StartDate = record.Milestone2StartDate;
        existingRecord.Milestone2CompletionDate = record.Milestone2CompletionDate;
        existingRecord.ClientName = record.ClientName;
        existingRecord.AgentName = record.AgentName;

        return Task.FromResult<Record?>(existingRecord);
    }

    public Task<bool> DeleteRecordAsync(int id)
    {
        var record = _records.FirstOrDefault(r => r.Id == id);
        if (record == null)
            return Task.FromResult(false);

        _records.Remove(record);
        return Task.FromResult(true);
    }
}
