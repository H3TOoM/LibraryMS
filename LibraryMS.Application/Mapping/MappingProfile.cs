using AutoMapper;
using LibraryMS.Application.DTOs.Books;
using LibraryMS.Application.DTOs.BorrowRecords;
using LibraryMS.Application.DTOs.Categories;
using LibraryMS.Application.DTOs.Fines;
using LibraryMS.Application.DTOs.Members;
using LibraryMS.Domain.Entities;

namespace LibraryMS.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile defining object-to-object mappings between DTOs and entities
    /// Configures mapping rules for all data transfer operations in the application
    /// </summary>
    public sealed class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the MappingProfile
        /// Defines all mapping configurations for the application
        /// </summary>
        public MappingProfile()
        {
            #region Book Mappings

            // Book DTO to Entity mappings
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();

            // If Field in Dto is null ignore it while mapping to Entity
            CreateMap<BookUpdateDto, Book>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity to DTO mapping with category name resolution
            CreateMap<Book, BookReadDto>()
                .ForCtorParam(nameof(BookReadDto.CategoryName), opt =>
                    opt.MapFrom(src => src.Category != null ? src.Category.Name : null));

            #endregion

            #region Category Mappings

            // Category DTO to Entity mappings
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryReadDto>();

            #endregion

            #region Member Mappings

            // Entity to DTO mapping
            CreateMap<Member, MemberReadDto>();

            // DTO to Entity mappings (ignore password hash for security)
            CreateMap<MemberCreateDto, Member>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<MemberUpdateDto, Member>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            #endregion

            #region Borrow Record Mappings

            // Create DTO mapping (ignore auto-generated and navigation properties)
            CreateMap<BorrowRecordCreateDto, BorrowRecord>()
                .ForMember(dest => dest.BorrowedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ReturnedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FineAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore())
                .ForMember(dest => dest.Member, opt => opt.Ignore());

            // Update DTO mapping (ignore immutable and navigation properties)
            CreateMap<BorrowRecordUpdateDto, BorrowRecord>()
                .ForMember(dest => dest.BookId, opt => opt.Ignore())
                .ForMember(dest => dest.MemberId, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowedAt, opt => opt.Ignore())
                .ForMember(dest => dest.FineAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Book, opt => opt.Ignore())
                .ForMember(dest => dest.Member, opt => opt.Ignore());

            // Entity to DTO mapping
            CreateMap<BorrowRecord, BorrowRecordReadDto>();

            #endregion

            #region Fine Mappings

            // Create DTO mapping (ignore auto-managed properties)
            CreateMap<FineCreateDto, Fine>()
                .ForMember(dest => dest.IsPaid, opt => opt.Ignore())
                .ForMember(dest => dest.PaidAt, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowRecord, opt => opt.Ignore());

            // Update DTO mapping with conditional PaidAt mapping
            CreateMap<FineUpdateDto, Fine>()
                .ForMember(dest => dest.BorrowRecordId, opt => opt.Ignore())
                .ForMember(dest => dest.BorrowRecord, opt => opt.Ignore())
                .ForMember(dest => dest.PaidAt, opt =>
                {
                    opt.Condition(src => src.PaidAt.HasValue);
                    opt.MapFrom(src => src.PaidAt!.Value);
                });

            // Entity to DTO mapping with conditional PaidAt
            CreateMap<Fine, FineReadDto>()
                .ForCtorParam(nameof(FineReadDto.PaidAt), opt => opt.MapFrom(src => src.IsPaid ? (DateTime?)src.PaidAt : null));

            #endregion
        }
    }
}
