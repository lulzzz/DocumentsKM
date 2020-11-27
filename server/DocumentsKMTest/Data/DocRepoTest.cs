using System.Collections.Generic;
using System.Linq;
using DocumentsKM.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsKM.Data
{
    public class SqlDocRepo : IDocRepo
    {
        private readonly ApplicationContext _context;

        public SqlDocRepo(ApplicationContext context)
        {
            _context = context;
        }

        public Doc GetById(int id)
        {
            return _context.Docs.FirstOrDefault(d => d.Id == id);
        }

        public Doc GetByUniqueKeyValues(int markId, int num, int docTypeId)
        {
            return _context.Docs.FirstOrDefault(d => (d.Mark.Id == markId) && (d.Num == num) && (d.Type.Id == docTypeId));
        }

        public IEnumerable<Doc> GetAllByMarkId(int markId)
        {
            return _context.Docs.Where(d => d.Mark.Id == markId).ToList();
        }

        public IEnumerable<Doc> GetAllByMarkIdAndDocType(int markId, int docTypeId)
        {
            return _context.Docs.Where(d => (d.Mark.Id == markId) && (d.Type.Id == docTypeId)).ToList();
        }

        public IEnumerable<Doc> GetAllByMarkIdAndNotDocType(int markId, int docTypeId)
        {
            return _context.Docs.Where(d => (d.Mark.Id == markId) && (d.Type.Id != docTypeId)).ToList();
        }

        public void Add(Doc doc)
        {
            _context.Docs.Add(doc);
            _context.SaveChanges();
        }

        public void Update(Doc doc)
        {
            _context.Entry(doc).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Doc doc)
        {
            _context.Docs.Remove(doc);
            _context.SaveChanges();
        }
    }
}