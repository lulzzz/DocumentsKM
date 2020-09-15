using System;
using System.Collections.Generic;
using System.Linq;
using DocumentsKM.Model;

namespace DocumentsKM.Data
{
    public class SqlMarkRepo : IMarkRepo
    {
        private readonly MarkContext _context;

        public SqlMarkRepo(MarkContext context)
        {
            _context = context;
        }

        public void CreateMark(Mark mark)
        {
            if (mark == null)
            {
                throw new ArgumentNullException(nameof(mark));
            }
            _context.Marks.Add(mark);
        }

        public IEnumerable<Mark> GetAllMarks()
        {
            return _context.Marks.ToList();
        }

        public Mark GetMarkById(ulong id)
        {
            return _context.Marks.FirstOrDefault(m => m.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateMark(Mark mark)
        {
            // Nothing
        }
    }
}