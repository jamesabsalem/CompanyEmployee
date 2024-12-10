using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace CompanyEmployee
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            return typeof(CompanyDto).IsAssignableFrom(type) ||
                   typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type) ||
                   base.CanWriteType(type);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            switch (context.Object)
            {
                case IEnumerable<CompanyDto> companies:
                {
                    foreach (var company in companies)
                    {
                        AppendCsvRow(buffer, company);
                    }

                    break;
                }
                case CompanyDto company:
                    AppendCsvRow(buffer, company);
                    break;
            }

            await response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private static void AppendCsvRow(StringBuilder buffer, CompanyDto company)
        {
            // Escaping any quotes in fields to ensure valid CSV formatting
            var escapedName = company.Name?.Replace("\"", "\"\"");
            var escapedAddress = company.FullAddress?.Replace("\"", "\"\"");

            buffer.AppendLine($"{company.Id},\"{escapedName}\",\"{escapedAddress}\"");
        }
    }
}