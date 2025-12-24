using AutoMapper;
using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.Interfaces;
using LibraryMS.Domain.Entities;
using LibraryMS.Domain.Repoistries;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMS.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IMainRepoistery<Book> _mainRepoistory;
        private readonly IBookRepoistory _bookRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IMainRepoistery<Book> mainRepoistory, IBookRepoistory bookRepoistory, IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mainRepoistory = mainRepoistory;
            _bookRepoistory = bookRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<int> CreateAsync(BookCreateDto dto)
        {
            var book = _mapper.Map<Book>(dto);
            await _mainRepoistory.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return book.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _mainRepoistory.GetByIdAsync(id);
            if (book == null) return false;
            await _mainRepoistory.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<BookReadDto>> GetAllAsync()
        {
            var books = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<IEnumerable<BookReadDto>> GetByCategoryIdAsync(int categoryId)
        {
            var books = await _bookRepoistory.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<BookReadDto?> GetByIdAsync(int id)
        {
            var book = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<BookReadDto?>(book);
        }

        public async Task<IEnumerable<BookReadDto>> SearchAsync(string query)
        {
            var books = await _bookRepoistory.SearchAsync(query);
            return _mapper.Map<IEnumerable<BookReadDto>>(books);
        }

        public async Task<bool> UpdateAsync(int id, BookUpdateDto dto)
        {
            var book = await _mainRepoistory.GetByIdAsync(id);
            if (book == null) return false;

            _mapper.Map(dto, book);
            await _mainRepoistory.UpdateAsync(id, book);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
