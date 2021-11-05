// assets
import { IconBrandChrome, IconHelp, IconSitemap, IconUsers } from '@tabler/icons';

// constant
const icons = {
    IconBrandChrome: IconBrandChrome,
    IconHelp: IconHelp,
    IconSitemap: IconSitemap,
    IconUsers: IconUsers
};

//-----------------------|| SAMPLE PAGE & DOCUMENTATION MENU ITEMS ||-----------------------//

export const other = {
    id: 'sample-docs-roadmap',
    type: 'group',
    children: [
        {
            id: 'user',
            title: 'Customers',
            type: 'item',
            url: '/customer',
            icon: icons['IconUsers'],
            breadcrumbs: false
        }
    ]
};
