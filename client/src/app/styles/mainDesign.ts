import Lara from '@primeng/themes/lara';

export const mainDesign = {
  preset: Lara,
  options: {
    cssLayer: true,
    darkModeSelector: false
  },
  semantic: {
    primary: {
      50:  '#FFF1F4',
      100: '#FFD6E0',
      200: '#FFB3C6',
      300: '#FF8FAE',
      400: '#FF6B96',
      500: '#FF3F72',
      600: '#E63666',
      700: '#CC043D',
      800: '#A80033',
      900: '#7A0026'
    },
    focusRing: {
      width: '2px',
      style: 'solid',
      color: '#F5E100'
    }
  }
} as const;
