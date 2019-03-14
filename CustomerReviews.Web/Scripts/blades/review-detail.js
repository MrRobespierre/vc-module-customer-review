angular.module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.reviewDetailController',
        ['$scope', 'CustomerReviews.WebApi',
            function ($scope, reviewsApi) {
                var blade = $scope.blade;

                function updateFavoriteProperties() {
                    blade.isLoading = true;
                    reviewsApi.getFavoriteProperties({ productId: blade.currentEntity.productId },
                        function(results) {
                            blade.favoriteProperties = results;
                            blade.isLoading = false;
                        });
                }
            }]);