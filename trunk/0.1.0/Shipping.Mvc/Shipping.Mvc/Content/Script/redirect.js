
// put function into jQuery namespace
jQuery.redirect = function (url, params)
{
    url = url || window.location.href || '';
    url = url.match(/\?/) ? url : url + '?';

    for (var key in params)
    {
        var re = RegExp(';?&?' + key + '=?[^&;]*', 'g');
        url = url.replace(re, '');
        url += ';' + key + '=' + params[key];
    }

    // cleanup url 
    url = url.replace(/[;&]$/, '');
    url = url.replace(/\?[;&]/, '?');
    url = url.replace(/[;&]{2}/g, ';');
    url = url.replace(';', '&');

    window.location.replace(url);
};