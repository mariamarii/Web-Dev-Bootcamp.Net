import  { useState } from 'react';
import { Autocomplete, TextField, Box } from '@mui/material';

interface Country {
  code: string;
  label: string;
}

const countries: Country[] = [
  { code: 'US', label: 'United States' },
  { code: 'GB', label: 'United Kingdom' },
  { code: 'FR', label: 'France' },
  { code: 'DE', label: 'Germany' },
  { code: 'SA', label: 'Saudi Arabia' },
  { code: 'IN', label: 'India' },
  { code: 'JP', label: 'Japan' },
  { code: 'CN', label: 'China' },
];

const defaultCountry = countries.find((country) => country.code === 'US') || countries[0];

interface CountryFlagDropdownProps {
  onChange?: (country: Country | null) => void;
  isMobile?: boolean;
}

export default function CountryFlagDropdown({ onChange, isMobile = false }: CountryFlagDropdownProps) {
  const [value, setValue] = useState<Country>(defaultCountry);

  const handleChange = (newValue: Country | null) => {
    if (newValue) {
      setValue(newValue);
      if (onChange) {
        onChange(newValue);
      }
    }
  };

  return (
    <Autocomplete
      id="country-select-dropdown"
      value={value}
      options={countries}
      disableClearable
      getOptionLabel={(option) => option.label}
      onChange={(_, newValue) => handleChange(newValue)}
      renderOption={(props, option) => (
        <Box
          component="li"
          sx={{ '&.MuiAutocomplete-option': { justifyContent: 'center', padding: '8px' } }}
          {...props}
        >
          <img
            loading="lazy"
            width="24"
            src={`https://flagcdn.com/w40/${option.code.toLowerCase()}.png`}
            srcSet={`https://flagcdn.com/w80/${option.code.toLowerCase()}.png 2x`}
            alt={option.label}
          />
        </Box>
      )}
      renderInput={(params) => (
        <TextField
          {...params}
          InputProps={{
            ...params.InputProps,
            startAdornment: (
              <img
                loading="lazy"
                width="24"
                src={`https://flagcdn.com/w40/${value.code.toLowerCase()}.png`}
                srcSet={`https://flagcdn.com/w80/${value.code.toLowerCase()}.png 2x`}
                alt={value.label}
                style={{ marginRight: '4px' }}
              />
            ),
            style: {
              paddingLeft: 8,
              paddingRight: 8,
              cursor: 'pointer',
              width: isMobile ? '100%' : '60px',
            },
          }}
          label=""
          variant="outlined"
          size="small"
          sx={{
            '& .MuiOutlinedInput-input': {
              padding: '7px 0',
              opacity: 0,
              width: '0px',
            },
            '& .MuiOutlinedInput-notchedOutline': { border: '1px solid #e0e0e0' },
            '& .MuiInputLabel-root': { display: 'none' },
            width: isMobile ? '100%' : 'auto',
          }}
        />
      )}
      popupIcon={null}
    />
  );
}