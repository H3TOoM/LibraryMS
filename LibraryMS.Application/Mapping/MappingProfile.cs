using AutoMapper;
using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.DTOs.Categories;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<Book, BookReadDto>()
                .ForCtorParam(nameof(BookReadDto.CategoryName), opt =>
                    opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryReadDto>();

            CreateMap<Member, MemberReadDto>();
            CreateMap<MemberCreateDto, Member>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<MemberUpdateDto, Member>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<BorrowRecordCreateDto, BorrowRecord>()
                .ForMember(dest => dest.BorrowedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReturnedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FineAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore())
                .ForMember(dest => dest.Member, opt => opt.Ignore());

            CreateMap<BorrowRecordUpdateDto, BorrowRecord>()
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.MemberId, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FineAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore())
                .ForMember(dest => dest.Member, opt => opt.Ignore());

            CreateMap<BorrowRecord, BorrowRecordReadDto>();

            CreateMap<FineCreateDto, Fine>()
                .ForMember(dest => dest.IsPaid, opt => opt.Ignore())
                .ForMember(dest => dest.PaidAt, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowRecord, opt => opt.Ignore());

            CreateMap<FineUpdateDto, Fine>()
                .ForMember(dest => dest.BorrowRecordId, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowRecord, opt => opt.Ignore())
                .ForMember(dest => dest.PaidAt, opt =>
                {
                    opt.Condition(src => src.PaidAt.HasValue);
                    opt.MapFrom(src => src.PaidAt!.Value);
                });

            CreateMap<Fine, FineReadDto>()
                .ForCtorParam(nameof(FineReadDto.PaidAt), opt => opt.MapFrom(src => src.IsPaid ? (DateTime?)src.PaidAt : null));
        }
    }
}
