using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Gymby.Application.CommandModels;

public class PaymentData
{
    [JsonProperty("pa   yment_id")]
    public long PaymentId { get; set; }
    [JsonProperty("action")]
    public string? Action { get; set; }
    [JsonProperty("status")]
    public string? Status { get; set; }
    [JsonProperty("version")]
    public long Version { get; set; }
    [JsonProperty("type")]
    public string? Type { get; set; }
    [JsonProperty("paytype")]
    public string? Paytype { get; set; }
    [JsonProperty("public_key")]
    public string? PublicKey { get; set; }
    [JsonProperty("acq_id")]
    public long AcqId { get; set; }
    [JsonProperty("order_id")]
    public string? OrderId { get; set; }
    [JsonProperty("liqpay_order_id")]
    public string? LiqpayOrderId { get; set; }
    [JsonProperty("description")]
    public string? Description { get; set; }
    [JsonProperty("sender_card_mask2")]
    public string? SenderCardMask2 { get; set; }
    [JsonProperty("sender_card_bank")]
    public string? SenderCardBank { get; set; }
    [JsonProperty("sender_card_type")]
    public string? SenderCardType { get; set; }
    [JsonProperty("sender_card_country")]
    public int SenderCardCountry { get; set; }
    [JsonProperty("ip")]
    public string? Ip { get; set; }
    [JsonProperty("amount")]
    public double Amount { get; set; }
    [JsonProperty("currency")]
    public string? Currency { get; set; }
    [JsonProperty("sender_commission")]
    public double SenderCommission { get; set; }
    [JsonProperty("receiver_commission")]
    public double ReceiverCommission { get; set; }
    [JsonProperty("agent_commission")]
    public double AgentCommission { get; set; }
    [JsonProperty("amount_debit")]
    public double AmountDebit { get; set; }
    [JsonProperty("amount_credit")]
    public double AmountCredit { get; set; }
    [JsonProperty("commission_debit")]
    public double CommissionDebit { get; set; }
    [JsonProperty("commission_credit")]
    public double CommissionCredit { get; set; }
    [JsonProperty("currency_debit")]
    public string? CurrencyDebit { get; set; }
    [JsonProperty("currency_credit")]
    public string? CurrencyCredit { get; set; }
    [JsonProperty("sender_bonus")]
    public double SenderBonus { get; set; }
    [JsonProperty("amount_bonus")]
    public double AmountBonus { get; set; }
    [JsonProperty("mpi_eci")]
    public string? MpiEci { get; set; }
    [JsonProperty("is_3ds")]
    public bool Is3ds { get; set; }
    [JsonProperty("language")]
    public string? Language { get; set; }
    [JsonProperty("create_date")]
    public long CreateDate { get; set; }
    [JsonProperty("end_date")]
    public long EndDate { get; set; }
    [JsonProperty("transaction_id")]
    public long TransactionId { get; set; }

    public static JsonSerializerSettings GetJsonSerializerSettings()
    {
        return new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };
    }
}
