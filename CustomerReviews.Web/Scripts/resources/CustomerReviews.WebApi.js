angular.module('CustomerReviews.Web')
.factory('CustomerReviews.WebApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {
        search: { method: 'POST', url: 'api/customerReviews/search' },
        update: { method: 'PUT' },
        getFavoriteProperties: { method: 'GET', url: 'api/favoriteProperties/:productId', params: { productId: '@productId' }, isArray: true }
    });
}]);