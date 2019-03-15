angular.module('CustomerReviews.Web')
.factory('CustomerReviews.WebApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {
        search: { method: 'POST', url: 'api/customerReviews/search' },
        update: { method: 'POST', url: 'api/customerReviews' },
        getAverageProductRating: { method: 'GET', url: 'api/customerReviews/getAverageProductRating/:productId', params: { productId: '@productId' }},
        getFavoriteProperties: { method: 'GET', url: 'api/favoriteProperties/:productId', params: { productId: '@productId' }, isArray: true }
    });
}]);