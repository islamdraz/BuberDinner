namespace BuberDinner.Domain.Models;

public abstract class ValueObject
{
    public abstract IEnumerable<object> GetEqualityComponents();

}
public class Price : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }
 
    public Price(
        decimal amount,
        string currency)
    {
        this.Amount = amount;
        this.Currency = currency;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return this.Amount;
        yield return this.Currency;
    }
}