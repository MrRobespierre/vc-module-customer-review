angular.module('CustomerReviews.Web').controller('CustomerReviews.Web.reviewsListController', ['$scope', 'CustomerReviews.WebApi', 'platformWebApp.bladeUtils', 'uiGridConstants', 'platformWebApp.uiGridHelper', 'platformWebApp.dialogService', function ($scope, reviewsApi, bladeUtils, uiGridConstants, uiGridHelper, dialogService) {
    $scope.uiGridConstants = uiGridConstants;

    var blade = $scope.blade;
    var bladeNavigationService = bladeUtils.bladeNavigationService;
    blade.headIcon = 'fa-comments';
    blade.toolbarCommands = [
        {
            name: "platform.commands.refresh", icon: 'fa fa-refresh',
            executeMethod: blade.refresh,
            canExecuteMethod: function () {
                return true;
            }
        },
        {
            name: "platform.commands.delete",
            icon: 'fa fa-trash-o',
            executeMethod: function () { deleteList($scope.gridApi.selection.getSelectedRows()); },
            canExecuteMethod: isItemsChecked,
            permission: 'catalog:delete'
        }
    ];

    blade.refresh = function () {
        blade.isLoading = true;
        reviewsApi.search(angular.extend(filter, {
            searchPhrase: filter.keyword ? filter.keyword : undefined,
            sort: uiGridHelper.getSortExpression($scope),
            skip: ($scope.pageSettings.currentPage - 1) * $scope.pageSettings.itemsPerPageCount,
            take: $scope.pageSettings.itemsPerPageCount
        }), function (data) {
            blade.isLoading = false;
            $scope.pageSettings.totalItems = data.totalCount;
            blade.currentEntities = data.results;
        });
    }

    blade.selectNode = function (data) {
        $scope.selectedNodeId = data.id;

        var newBlade = {
            id: 'reviewDetails',
            currentEntityId: data.id,
            currentEntity: data,
            title: 'customerReviews.blades.review-detail.labels.title',
            titleValues: { author: data.authorNickname },
            controller: 'CustomerReviews.Web.reviewDetailController',
            template: 'Modules/$(CustomerReviews.Web)/Scripts/blades/review-detail.tpl.html'
        };
        bladeNavigationService.showBlade(newBlade, blade);
    }

    function isItemsChecked() {
        return $scope.gridApi && _.any($scope.gridApi.selection.getSelectedRows());
    }

    function deleteList(selection) {
        var dialog = {
            id: 'confirmDeleteItems',
            title: 'customerReviews.dialogs.deleteReviews.title',
            message: 'customerReviews.dialogs.deleteReviews.message',
            callback: function (remove) {
                if (remove) {
                    var ids = _.pluck(selection, 'id');
                    reviewsApi.remove({ ids: ids }, function() {
                        blade.refresh();
                    });
                }
            }
        }
        dialogService.showConfirmationDialog(dialog);
    }

    // simple and advanced filtering
    var filter = $scope.filter = blade.filter || {};
    filter.criteriaChanged = function () {
        if ($scope.pageSettings.currentPage > 1) {
            $scope.pageSettings.currentPage = 1;
        } else {
            blade.refresh();
        }
    };

    // ui-grid
    $scope.setGridOptions = function (gridOptions) {
        uiGridHelper.initialize($scope, gridOptions, function (gridApi) {
            uiGridHelper.bindRefreshOnSortChanged($scope);
        });
        bladeUtils.initializePagination($scope);
    };

}]);
