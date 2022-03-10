import { MenuItemType } from '@paljs/ui/types';

const items: MenuItemType[] = [
  {
    title: 'Home Page',
    icon: { name: 'home' },
    link: { href: '/dashboard' },
  },
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
    title: 'Danh mục',
    icon: { name: 'people-outline' },
    children: [
      {
        title: 'Accordion',
        link: { href: '/admin/extra-components/accordion' },
      },
      {
        title: 'Actions',
        link: { href: '/admin/extra-components/actions' },
      },
      {
        title: 'Alert',
        link: { href: '/admin/extra-components/alert' },
      },
      {
        title: 'List',
        link: { href: '/admin/extra-components/list' },
      },
      {
        title: 'Spinner',
        link: { href: '/admin/extra-components/spinner' },
      },
      {
        title: 'Progress Bar',
        link: { href: '/admin/extra-components/progress' },
      },
      {
        title: 'Tabs',
        link: { href: '/admin/extra-components/tabs' },
      },
      {
        title: 'Chat',
        link: { href: '/admin/extra-components/chat' },
      },
      {
        title: 'Cards',
        link: { href: '/admin/extra-components/cards' },
      },
      {
        title: 'Flip Card',
        link: { href: '/admin/extra-components/flip-card' },
      },
      {
        title: 'Reveal Card',
        link: { href: '/admin/extra-components/reveal-card' },
      },
    ],
  },
  {
    title: 'Forms',
    icon: { name: 'edit-2-outline' },
    children: [
      {
        title: 'Inputs',
        link: { href: '/admin/forms/inputs' },
      },
      {
        title: 'Layout',
        link: { href: '/admin/forms/form-layout' },
      },
      {
        title: 'Buttons',
        link: { href: '/admin/forms/buttons' },
      },
      {
        title: 'Select',
        link: { href: '/admin/forms/select' },
      },
    ],
  },
  {
    title: 'UI Features',
    icon: { name: 'keypad-outline' },
    children: [
      {
        title: 'Grid',
        link: { href: '/admin/ui-features/grid' },
      },
      {
        title: 'Animated Searches',
        link: { href: '/admin/ui-features/search' },
      },
    ],
  },
  {
    title: 'Modal & Overlays',
    icon: { name: 'browser-outline' },
    children: [
      {
        title: 'Popover',
        link: { href: '/admin/modal-overlays/popover' },
      },
      {
        title: 'Tooltip',
        link: { href: '/admin/modal-overlays/tooltip' },
      },
      {
        title: 'Toastr',
        link: { href: '/admin/modal-overlays/toastr' },
      },
    ],
  },
  {
    title: 'Editors',
    icon: { name: 'text-outline' },
    children: [
      {
        title: 'TinyMCE',
        link: { href: '/admin/editors/tinymce' },
      },
      {
        title: 'CKEditor',
        link: { href: '/admin/editors/ckeditor' },
      },
    ],
  },
  {
    title: 'Miscellaneous',
    icon: { name: 'shuffle-2-outline' },
    children: [
      {
        title: '404',
        link: { href: '/admin/miscellaneous/404' },
      },
    ],
  },
  {
    title: 'Auth',
    icon: { name: 'lock-outline' },
    children: [
      {
        title: 'Login',
        link: { href: '/auth/login' },
      },
      {
        title: 'Register',
        link: { href: '/auth/register' },
      },
      {
        title: 'Request Password',
        link: { href: '/auth/request-password' },
      },
      {
        title: 'Reset Password',
        link: { href: '/auth/reset-password' },
      },
    ],
  },
];

export default items;
