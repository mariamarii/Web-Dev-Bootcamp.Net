
import {
  Drawer,
  List,
  ListItemIcon,
  ListItemButton,
  Box,
  Avatar,
  Skeleton,
  useMediaQuery,
  useTheme,
} from '@mui/material';
import GridViewOutlinedIcon from '@mui/icons-material/GridViewOutlined';
import PeopleOutlineIcon from '@mui/icons-material/PeopleOutline';
import DescriptionOutlinedIcon from '@mui/icons-material/DescriptionOutlined';
import FolderOutlinedIcon from '@mui/icons-material/FolderOutlined';
import CalendarTodayOutlinedIcon from '@mui/icons-material/CalendarTodayOutlined';
import AutoAwesomeMosaicOutlinedIcon from '@mui/icons-material/AutoAwesomeMosaicOutlined';
import SettingsOutlinedIcon from '@mui/icons-material/SettingsOutlined';

interface MyDrawerProps {
  open: boolean;
  onClose: () => void;
  isLoading?: boolean; 
}

const MyDrawer = ({ open, onClose, isLoading = false }: MyDrawerProps) => {
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm')); // <600px
  const isTablet = useMediaQuery(theme.breakpoints.between('sm', 'md')); // 600px–960px

  const drawerWidth = isMobile ? 48 : isTablet ? 50 : 60;

  const logo = (
    <Box className="flex justify-center" sx={{ mt: 1 }}>
      <Box
        className="w-8 h-8 bg-blue-500 rounded-md flex items-center justify-center"
        sx={{ width: isMobile ? 24 : 32, height: isMobile ? 24 : 32 }}
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          className="h-5 w-5 text-white"
          viewBox="0 0 20 20"
          fill="currentColor"
          width={isMobile ? 16 : 20}
          height={isMobile ? 16 : 20}
        >
          <path d="M2 11a1 1 0 011-1h2a1 1 0 011 1v5a1 1 0 01-1 1H3a1 1 0 01-1-1v-5zm6-4a1 1 0 011-1h2a1 1 0 011 1v9a1 1 0 01-1 1H9a1 1 0 01-1-1V7zm6-3a1 1 0 011-1h2a1 1 0 011 1v12a1 1 0 01-1 1h-2a1 1 0 01-1-1V4z" />
        </svg>
      </Box>
    </Box>
  );

  const navItems = [
    { icon: <GridViewOutlinedIcon />, id: 0 },
    { icon: <AutoAwesomeMosaicOutlinedIcon />, id: 1 },
    { icon: <PeopleOutlineIcon />, color: 'text-blue-600', id: 2 },
    { icon: <DescriptionOutlinedIcon />, id: 3 },
    { icon: <FolderOutlinedIcon />, id: 4 },
    { icon: <CalendarTodayOutlinedIcon />, id: 5 },
  ];

  if (isLoading) {
    return (
      <Box
        sx={{
          width: drawerWidth,
          height: 'calc(100vh - 16px)',
          top: 8,
          left: 0,
          backgroundColor: '#fff',
          border: '1px solid #e0e0e0',
          borderRadius: '20px',
          paddingY: isMobile ? 1.5 : 2, 
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'space-between',
          boxSizing: 'border-box',
        }}
      >
        <Box className="flex flex-col items-center" sx={{ mt: 1 }}>
          <Skeleton variant="rectangular" width={isMobile ? 24 : 32} height={isMobile ? 24 : 32} />
        </Box>
        <Box
          sx={{
            flex: 1,
            display: 'flex',
            flexDirection: 'column',
            justifyContent: 'center',
          }}
        >
          <List disablePadding>
            {[...Array(6)].map((_, index) => (
              <Skeleton
                key={index}
                variant="rectangular"
                width={isMobile ? 32 : 36}
                height={isMobile ? 32 : 36}
                sx={{ my: 1, mx: 'auto', borderRadius: '8px' }}
              />
            ))}
          </List>
        </Box>
        <Box
          className="flex flex-col items-center"
          sx={{ spaceY: isMobile ? 1.5 : 2, mb: 1 }}
        >
          <Skeleton variant="circular" width={isMobile ? 24 : 28} height={isMobile ? 24 : 28} />
          <Skeleton
            variant="rectangular"
            width={isMobile ? 32 : 36}
            height={isMobile ? 32 : 36}
            sx={{ borderRadius: '8px' }}
          />
        </Box>
      </Box>
    );
  }

  const drawerContent = (
    <>
      <Box className="flex flex-col items-center" sx={{ mt: 1 }}>
        {logo}
      </Box>
      <Box
        sx={{
          flex: 1,
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'center',
        }}
      >
        <List disablePadding>
          {navItems.map((item) => (
            <ListItemButton
              key={item.id}
              sx={{
                padding: 0, 
                display: 'flex',
                justifyContent: 'center', 
              }}
            >
              <ListItemIcon
                sx={{
                  minWidth: 0,
                  display: 'flex',
                  justifyContent: 'center', 
                }}
              >
                <span
                  className={`
                    text-xl 
                    my-1 
                    rounded-xl 
                    flex 
                    items-center 
                    justify-center 
                    transition-all
                    ${item.color || ''}
                    ${item.id === 2 ? 'bg-blue-600 text-white' : 'hover:bg-gray-100 text-gray-600'}
                  `}
                  style={{
                    fontSize: isMobile ? '16px' : '20px',
                    padding: isMobile ? '6px' : '8px', 
                    width: isMobile ? '32px' : '36px',
                    height: isMobile ? '32px' : '36px', 
                  }}
                >
                  {item.icon}
                </span>
              </ListItemIcon>
            </ListItemButton>
          ))}
        </List>
      </Box>
      <Box
        className="flex flex-col items-center"
        sx={{ spaceY: isMobile ? 1.5 : 2, mb: 1 }}
      >
        <Avatar
          className="bg-gray-800 text-white text-sm"
          sx={{ width: isMobile ? 28 : 36, height: isMobile ? 28 : 36 }}
        >
          MM
        </Avatar>
        <ListItemButton
          className="rounded-xl hover:bg-gray-100"
          sx={{ padding: isMobile ? '4px 8px' : '8px 12px' }}
        >
          <ListItemIcon className="min-w-0 justify-center">
            <SettingsOutlinedIcon
              className="text-gray-500"
              sx={{ fontSize: isMobile ? 20 : 24 }}
            />
          </ListItemIcon>
        </ListItemButton>
      </Box>
    </>
  );

  return (
    <Drawer
      variant={isMobile ? 'temporary' : 'permanent'}
      anchor="left"
      open={isMobile ? open : true}
      onClose={onClose}
      sx={{
        '& .MuiDrawer-paper': {
          width: drawerWidth,
          height: 'calc(100vh - 16px)',
          top: 8,
          left: 8,
          backgroundColor: '#fff',
          border: '1px solid #e0e0e0',
          borderRadius: '20px',
          paddingY: isMobile ? 1.5 : 2, 
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'space-between',
          overflow: 'hidden',
          boxSizing: 'border-box',
        },
      }}
    >
      {drawerContent}
    </Drawer>
  );
};

export default MyDrawer;