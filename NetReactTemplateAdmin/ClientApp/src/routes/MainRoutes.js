import React, { lazy } from 'react';
import { Route, Switch, useLocation } from 'react-router-dom';

// project imports
import MainLayout from './../layout/MainLayout';
import Loadable from '../ui-component/Loadable';

// dashboard routing
const DashboardDefault = Loadable(lazy(() => import('../views/dashboard/Default')));

// Customer
const CustomerView = Loadable(lazy(() => import('../views/customer')));
const CustomerEditView = Loadable(lazy(() => import('../views/customer/customer-edit')));
const CustomerNewView = Loadable(lazy(() => import('../views/customer/customer-new')));

//Product
const ProductView = Loadable(lazy(() => import('../views/product')));
const ProductEditView = Loadable(lazy(() => import('../views/product/product-edit')));
const ProductNewView = Loadable(lazy(() => import('../views/product/product-new')));

//Order
const OrderView = Loadable(lazy(() => import('../views/order')));
const OrderEditView = Loadable(lazy(() => import('../views/order/order-edit')));
const OrderNewView = Loadable(lazy(() => import('../views/order/order-new')));

const MainRoutes = () => {
    const location = useLocation();

    return (
        <Route
            path={[
                '/dashboard/default',

                '/customer',
                '/customer-edit',
                '/customer-new',

                '/product',
                '/product-edit',
                '/product-new',

                '/order',
                '/order-edit',
                '/order-new',
            ]}
        >
            <MainLayout>
                <Switch location={location} key={location.pathname}>
                    <Route path="/dashboard/default" component={DashboardDefault} />

                    <Route path="/customer" component={CustomerView} />
                    <Route path="/customer-edit" component={CustomerEditView} />
                    <Route path="/customer-new" component={CustomerNewView} />

                    <Route path="/product" component={ProductView} />
                    <Route path="/product-edit" component={ProductEditView} />
                    <Route path="/product-new" component={ProductNewView} />

                    <Route path="/order" component={OrderView} />
                    <Route path="/order-edit" component={OrderEditView} />
                    <Route path="/order-new" component={OrderNewView} />
                </Switch>
            </MainLayout>
        </Route>
    );
};

export default MainRoutes;
