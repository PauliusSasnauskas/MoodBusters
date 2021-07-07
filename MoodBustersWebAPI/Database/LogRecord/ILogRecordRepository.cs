using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoodBustersWebAPI.Database
{
    interface ILogRecordRepository
    {
        LogRecord GetLogRecord(int id);
        IEnumerable<LogRecord> GetAllLogRecords();
        void Add(LogRecord logRecord);
        void Update(LogRecord logRecordChanges);
        void Delete(int Id);
    }
}
