import { Box, IconButton, Skeleton, useMediaQuery, useTheme } from '@mui/material';
import MailOutlineRoundedIcon from '@mui/icons-material/MailOutlineRounded';
import NotificationsNoneRoundedIcon from '@mui/icons-material/NotificationsNoneRounded';
import MenuIcon from '@mui/icons-material/Menu';
import CountryFlagDropdown from './CountryFlagDropdown';

interface Country {
  code: string;
  label: string;
}

interface TopBarProps {
  onCountryChange?: (country: Country | null) => void;
  onToggleSidebar: () => void;
  showHamburger: boolean;
  isLoading?: boolean;
  isMobile?: boolean;

}

function TopBar({ onCountryChange, onToggleSidebar, showHamburger, isLoading = false }: TopBarProps) {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const isTablet = useMediaQuery(theme.breakpoints.between('sm', 'md'));

  if (isLoading) {
    return (
      <Box
        className="bg-white shadow-sm rounded-lg"
        sx={{
          px: { xs: 4, sm: 6 },
          py: { xs: 2, sm: 4 },
          display: 'flex',
          flexDirection: { xs: 'column', sm: 'row' },
          alignItems: { xs: 'flex-start', sm: 'center' },
          gap: { xs: 2, sm: 0 },
          justifyContent: 'space-between',
          height: { xs: 'auto', sm: 64 },
        }}
      >
        <Box display="flex" alignItems="center" gap={2}>
          {showHamburger && (
            <Skeleton variant="rectangular" width={40} height={40} sx={{ display: { sm: 'none' } }} />
          )}
          <Box display="flex" alignItems="center" gap={1}>
            <Skeleton variant="rectangular" width={32} height={32} />
            <Box>
              <Skeleton variant="text" width={120} height={24} />
              <Skeleton variant="text" width={150} height={16} />
            </Box>
          </Box>
        </Box>
        <Box display="flex" alignItems="center" gap={2}>
          <Skeleton variant="circular" width={24} height={24} />
          <Skeleton variant="circular" width={24} height={24} />
          <Skeleton variant="rectangular" width={120} height={32} />
        </Box>
      </Box>
    );
  }

  return (
    <div
      className="bg-white shadow-sm rounded-lg"
      style={{
        padding: isMobile ? '12px 16px' : isTablet ? '16px 20px' : '16px 24px',
        display: 'flex',
        flexDirection: isMobile ? 'column' : 'row',
        alignItems: isMobile ? 'flex-start' : 'center',
        gap: isMobile ? '12px' : '0',
        justifyContent: 'space-between',
        height: isMobile ? 'auto' : '64px',
      }}
    >
      <div className="flex items-center">
        {showHamburger && (
          <IconButton
            onClick={onToggleSidebar}
            sx={{ display: { sm: 'none' }, mr: 1, p: 0.5 }}
          >
            <MenuIcon fontSize="medium" />
          </IconButton>
        )}
        <div className="flex items-center">
          <div
            className="w-8 h-8 bg-blue-100 rounded-md flex items-center justify-center mr-3"
            style={{ width: isMobile ? 24 : 32, height: isMobile ? 24 : 32 }}
          >
            <div
              className="text-blue-600"
              style={{ fontSize: isMobile ? 16 : 20 }}
            >
              <svg viewBox="0 0 24 24" fill="currentColor" width={isMobile ? 20 : 24} height={isMobile ? 20 : 24}>
                <path d="M5 3v18h14V3H5zm12 16H7V5h10v14z" />
                <path d="M9 7h6v2H9zM9 11h6v2H9zM9 15h4v2H9z" />
              </svg>
            </div>
          </div>
          <div>
            <h1
              className="font-bold text-gray-800"
              style={{ fontSize: isMobile ? '1rem' : isTablet ? '1.125rem' : '1.25rem' }}
            >
              Team List
            </h1>
            <div className="flex text-gray-500" style={{ fontSize: isMobile ? '0.625rem' : '0.75rem' }}>
              <span className="mr-2 text-blue-500">Admin Dashboard</span>
              <span className="text-gray-400">•</span>
              <span className="ml-2">Team List</span>
            </div>
          </div>
        </div>
      </div>

      <div
        className="flex items-center"
        style={{ gap: isMobile ? '8px' : isTablet ? '12px' : '16px' }}
      >
        <button
          className="p-1 text-gray-500 hover:bg-gray-100 rounded-full relative"
          style={{ padding: isMobile ? '4px' : '8px' }}
        >
          <span
            className="absolute top-0 right-0 bg-red-500 rounded-full text-white flex items-center justify-center"
            style={{
              width: isMobile ? 14 : 16,
              height: isMobile ? 14 : 16,
              fontSize: isMobile ? '0.625rem' : '0.75rem',
            }}
          >
            2
          </span>
          <NotificationsNoneRoundedIcon fontSize={isMobile ? 'small' : 'medium'} />
        </button>

        <button
          className="p-1 text-gray-500 hover:bg-gray-100 rounded-full"
          style={{ padding: isMobile ? '4px' : '8px' }}
        >
          <MailOutlineRoundedIcon fontSize={isMobile ? 'small' : 'medium'} />
        </button>

        <div style={{ width: isMobile ? '100%' : 'auto' }}>
          <CountryFlagDropdown onChange={onCountryChange} isMobile={isMobile} />
        </div>
      </div>
    </div>
  );
}

export default TopBar;