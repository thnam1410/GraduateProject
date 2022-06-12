using GraduateProject.Domain.Common;

namespace GraduateProject.Domain.AppEntities.Entities;

public class CrawlRoute
{
    public double Distance { get; set; } //: 8900
    public string? Headway { get; set; } //: "18 - 26"

    public string?
        InBoundDescription
    {
        get;
        set;
    } //: "Bến xe buýt Chợ Lớn A-Lê Quang Sung-Nguyễn Hữu Thận-Tháp Mười-Hải Thượng Lãn Ông-Châu Văn Liêm-Nguyễn Trãi-Nguyễn Tri Phương-Trần Phú-Trần Hưng Đạo -Hàm Nghi-Hồ Tùng Mậu-đường nhánh S2-Tôn Đức Thắng-Hai Bà Trưng-Đông Du-Thi Sách-Công trường Mê Linh¿"

    public string? InBoundName { get; set; } //: "Bến Thành¿"
    public string? NumOfSeats { get; set; } //: "15"
    public string? OperationTime { get; set; } //: "05:00 - 19:00"
    public string? Orgs { get; set; } //: "Cty TNHH Du lịch, Dịch vụ Xây dựng Bảo Yến, ĐT: 028.3776.3777<br/>"

    public string?
        OutBoundDescription
    {
        get;
        set;
    } //: "Công trường Mê Linh-Thi Sách-Công trường Mê Linh-Tôn Đức Thắng-Hàm Nghi - Phó Đức Chính - Nguyễn Thái Bình - Ký Con - Trần Hưng Đạo-Nguyễn Tri Phương-Trần Phú-Trần Hưng Đạo-Châu Văn Liêm-Hải Thượng Lãn Ông-Trang Tử-Bến xe buýt Chợ Lớn A¿"

    public string? OutBoundName { get; set; } //: "Bến Xe buýt Chợ Lớn¿"
    public int RouteId { get; set; } //: 1
    public string? RouteName { get; set; } //: "Bến Thành - Bến Xe buýt Chợ Lớn"
    public string? RouteNo { get; set; } //: "01"

    public string?
        Tickets
    {
        get;
        set;
    } //: "<br/>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp- Vé lượt trợ giá: 5,000 VNĐ<br/>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp- Vé lượt trợ giá HSSV: 3,000 VNĐ<br/>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp- Vé tập: 112,500 VNĐ"

    public string? TimeOfTrip { get; set; } //: "35"
    public string? TotalTrip { get; set; } //: "86 [TPD]"
    public string? Type { get; set; } //: "Phổ thông - Có trợ giá"
}