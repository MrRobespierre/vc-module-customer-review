angular.module('CustomerReviews.Web').controller('CustomerReviews.Web.reviewDetailController', ['$scope', 'CustomerReviews.WebApi', function ($scope, reviewsApi) {
    var blade = $scope.blade;

    blade.refresh = function (parentRefresh) {
        blade.isLoading = true;
        reviewsApi.getFavoriteProperties({ productId: blade.currentEntity.productId },
            function (results) {
                blade.favoriteProperties = results;

                blade.origItem = blade.currentEntity;
                blade.item = angular.copy(blade.currentEntity);
                blade.currentEntity = blade.item;
                blade.isLoading = false;

                if (parentRefresh && blade.parentBlade.refresh) {
                    blade.parentBlade.refresh();
                }
            });
    }

    function resetChanges() {
        angular.copy(blade.origItem, blade.item);
    }

    function saveChanges() {
        blade.isLoading = true;
        reviewsApi.update([blade.item], function() {
            blade.isLoading = false;
            blade.refresh(true);
        });
    };

    function isDirty() {
        return !angular.equals(blade.item, blade.origItem) && blade.hasUpdatePermission();
    };

    function canSave() {
        return isDirty();
    }

    blade.toolbarCommands = [
        {
            name: "platform.commands.save",
            icon: 'fa fa-save',
            executeMethod: saveChanges,
            canExecuteMethod: canSave,
            permission: blade.updatePermission
        },
        {
            name: "platform.commands.reset",
            icon: 'fa fa-undo',
            executeMethod: resetChanges,
            canExecuteMethod: isDirty,
            permission: blade.updatePermission
        }
    ];

    blade.refresh(false);
}]);