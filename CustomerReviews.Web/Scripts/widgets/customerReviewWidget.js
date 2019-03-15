angular.module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.customerReviewWidgetController', ['$scope', 'CustomerReviews.WebApi', 'platformWebApp.bladeNavigationService', function ($scope, reviewsApi, bladeNavigationService) {
        var blade = $scope.blade;
        var filter = { };

        function refresh() {
            $scope.loading = true;
            reviewsApi.getAverageProductRating({ productId: filter.productId }, function (data) {
                $scope.loading = false;

                $scope.totalCount = data.reviewsCount;
                $scope.averageRating = data.rating;
            });
        }

        $scope.openBlade = function () {
            if ($scope.loading || !$scope.totalCount)
                return;

            var newBlade = {
                id: "reviewsList",
                filter: filter,
                title: 'customerReviews.blades.review-list.labels.title',
                titleValues: { sku: blade.title },
                controller: 'CustomerReviews.Web.reviewsListController',
                template: 'Modules/$(CustomerReviews.Web)/Scripts/blades/reviews-list.tpl.html'
            };
            bladeNavigationService.showBlade(newBlade, blade);
        };

        $scope.$watch("blade.itemId", function (id) {
            filter.productId = id;

            if (id) refresh();
        });
    }]);
