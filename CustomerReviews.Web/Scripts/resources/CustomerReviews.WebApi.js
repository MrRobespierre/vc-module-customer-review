angular.module('CustomerReviews.Web')
.factory('CustomerReviews.WebApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {
        search: { method: 'POST', url: 'api/customerReviews/search' },
        update: { method: 'POST', url: 'api/customerReviews' },
        getFavoriteProperties: { method: 'GET', url: 'api/favoriteProperties/:productId', params: { productId: '@productId' }, isArray: true }
    });
}]);