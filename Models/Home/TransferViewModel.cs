namespace ASP_SPD_222.Models.Home
{
     /*
     * Модель з даними, необхідна для відображення
     * сторінки /Home/Transfer
     */
    public record TransferViewModel
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public String ControllerName { get; set; } = null!;
        public TransferFormModel? formModel { get; set; }
    }
}
