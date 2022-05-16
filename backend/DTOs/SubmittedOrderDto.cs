namespace foodies_app.DTOs;

public class SubmittedOrderDto
{
    public int tableId { get; set; }
    public DateTime time  {get;set;}

    public  ICollection <SubmittedProductDto[]> products { get; set; }
   
}