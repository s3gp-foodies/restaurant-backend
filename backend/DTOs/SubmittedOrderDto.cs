namespace foodies_app.DTOs;

public class SubmittedOrderDto
{
    public int tableId { get; set; }
    public DateTime time  {get;set;}

    public  List <SubmittedProductDto> products { get; set; }
   
}