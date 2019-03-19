angular.module('CustomerReviews.Web')
.factory('CustomerReviews.WebApi', ['$resource', function ($resource) {
    return $resource('api/customerReviews', {}, {
        search: { method: 'POST', url: 'api/customerReviews/search' },
        update: { method: 'POST', url: 'api/customerReviews' },
        remove: { method: 'DELETE', url: 'api/customerReviews' },
        getAverageProductRating: { method: 'GET', url: 'api/customerReviews/:productId/averageProductRating', params: { productId: '@productId' }},
        getFavoriteProperties: { method: 'GET', url: 'api/customerReviews/:productId/favoriteProperties', params: { productId: '@productId' }, isArray: true }
    });
}]);