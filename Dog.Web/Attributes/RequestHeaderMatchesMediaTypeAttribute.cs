using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace Dog.Web.Attributes;


public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
{
    private readonly string _header;
    private readonly MediaTypeCollection _mediaTypes = new();

    /// <summary>
    /// Инициализация атрибута, который фильтрует помогает определить подходит ли метод под конкретный заголовок запроса.
    /// </summary>
    /// <param name="header">Заголовок запроса.</param>
    /// <param name="headerValue">Значение типа.</param>
    /// <param name="otherMediaTypes">Значение других допустимых media-types.</param>
    /// <exception cref="ArgumentNullException">Если не передан заголовок.</exception>
    /// <exception cref="ArgumentException">Если передано некорректное значение заголовка.</exception>
    public RequestHeaderMatchesMediaTypeAttribute(string header, string headerValue, params string[] otherMediaTypes)
    {
        _header = header ?? throw new ArgumentNullException(nameof(header));
        if (MediaTypeHeaderValue.TryParse(headerValue, out var mediaType))
        {
            _mediaTypes.Add(mediaType);
        }
        else
        {
            throw new ArgumentException($"'{headerValue}' is not a valid media type.");
        }

        foreach (var mType in otherMediaTypes)
        {
            if (MediaTypeHeaderValue.TryParse(mType, out mediaType))
            {
                _mediaTypes.Add(mediaType);
            }
            else
            {
                throw new ArgumentException($"'{mType}' is not a valid media type.");
            }

        }
        
    }
    ///<inheritdoc />
    public bool Accept(ActionConstraintContext context)
    {
        var headers = context.RouteContext.HttpContext.Request.Headers;
        if(!headers.ContainsKey(_header))
            return false;
        var headerValue = new MediaType(headers[_header]);
        foreach (var mediaType in _mediaTypes)
        {
            var parsedMediaType = new MediaType(mediaType);
            if(headerValue.Equals(parsedMediaType))
                return true;
        }
        return false;
    }
    ///<inheritdoc />
    public int Order { get; }
}