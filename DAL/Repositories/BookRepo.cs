﻿using Bookush.DAL.DBO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookush.DAL.Repositories
{
    public class BookRepo : BaseRepo, IRepository<Book>
    {
        public BookRepo(BookDBContext context)
            :base(context)
        {
        }

        public async Task CreateAsync(Book entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllAsync()
        {
           return await _context.Books.Include(b => b.Publisher).ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
           return await _context.Books
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Book entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public bool Exists (int id)
        {
            return _context.Books.Any(b => b.Id == id);
        }
    }
}
