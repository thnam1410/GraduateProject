import { MenuItemType } from '@paljs/ui/types';

const items: MenuItemType[] = [
  {
    title: 'Bộ lọc',
    group: true,
  },
  {
    title: 'Quản lý tài khoản ',
    icon: { name : 'people-outline'},
    link: {href: '/admin/account/user-account'}
  }
  ,
  {
    title: 'Quản lý danh sách BĐS ',
    icon: { name : 'keypad-outline'},
    link: {href: '/admin/real-estate-list/realEstate'}
  }
  ,
  {
    title: 'Quản lý',
    icon: { name: 'browser-outline' },
    children: [
      {
        title: 'Phê duyệt BĐS',
        link: {href: '/admin/management/realEstatemanagement'}
      }
      ,
      {
        title: 'Thống kê',
        link: {href: '/admin/management/statistical'}
      }
    ]
  }

];

export default items;
