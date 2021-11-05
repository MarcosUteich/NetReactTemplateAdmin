// assets
import { IconBrandFramer, IconTypography, IconPalette, IconShadow, IconWindmill, IconLayoutGridAdd } from '@tabler/icons';

// constant
const icons = {
    IconTypography: IconTypography,
    IconPalette: IconPalette,
    IconShadow: IconShadow,
    IconWindmill: IconWindmill,
    IconBrandFramer: IconBrandFramer,
    IconLayoutGridAdd: IconLayoutGridAdd
};

//-----------------------|| UTILITIES MENU ITEMS ||-----------------------//

export const utilities = {
    id: 'sales',
    title: 'Sales',
    type: 'group',
    children: [
        {
            id: 'icons',
            title: 'Sales',
            type: 'collapse',
            icon: icons['IconWindmill'],
            children: [
                {
                    id: 'orders',
                    title: 'Orders',
                    type: 'item',
                    url: '/order',
                    breadcrumbs: false
                },
                {
                    id: 'products',
                    title: 'Products',
                    type: 'item',
                    url: '/product',
                    breadcrumbs: false
                }
            ]
        }
    ]
};
