import { definePreset } from '@primeuix/themes';
import Aura from '@primeuix/themes/aura';

const mainDesign = definePreset(Aura, {
  semantic: {
    primary: {
      50: '{indigo.50}',
      100: '{indigo.100}',
      200: '{indigo.200}',
      300: '{indigo.300}',
      400: '{indigo.400}',
      500: '{indigo.500}',
      600: '{indigo.600}',
      700: '{indigo.700}',
      800: '{indigo.800}',
      900: '{indigo.900}',
      950: '{indigo.950}'
    },

    colorScheme: {   // ✅ כאן זה חייב להיות
      light: {
        surface: {
          0: '#ffffff',
          50: '{zinc.50}',
          100: '{zinc.100}',
          200: '{zinc.200}',
          300: '{zinc.300}',
          400: '{zinc.400}',
          500: '{zinc.500}',
          600: '{zinc.600}',
          700: '{zinc.700}',
          800: '{zinc.800}',
          900: '{zinc.900}',
          950: '{zinc.950}'
        }
      }
    }
  },

  extend: {
    brand: {
      accent: {
        500: '{amber.500}',
        600: '{amber.600}'
      }
    }
  },

  components: {
    button: {
      root: { borderRadius: '12px' }
    },
    inputtext: {
      root: { borderRadius: '12px' }
    },
    card: {
      root: { borderRadius: '16px' }
    }
  }
});

export default mainDesign;
