angular.module('CustomerReviews.Web')
    .controller('CustomerReviews.Web.reviewDetailController',
        ['$scope', 'CustomerReviews.WebApi', 'platformWebApp.bladeUtils', 'uiGridConstants', 'platformWebApp.uiGridHelper',
            function ($scope, reviewsApi, bladeUtils, uiGridConstants, uiGridHelper) {
                var blade = $scope.blade;
                var model = blade.currentEntity;

                blade.isLoading = false;
            }]);