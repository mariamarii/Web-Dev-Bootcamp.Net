import  { useState } from 'react';
import MyDrawer from '../components/MyDrawer';
import TopBar from '../components/Topbar';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import TeamListTable from '../components/TeamListTable';
import { useMediaQuery, useTheme } from '@mui/material';

// Define country interface
interface Country {
  code: string;
  label: string;
}
  
const queryClient = new QueryClient();

function Dashboard() {
  const [openSidebar, setOpenSidebar] = useState(false);
  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm')); // <600px

  // Simulate loading state (replace with actual API loading if needed)
  const isLoading = false;

  const handleToggleSidebar = () => {
    setOpenSidebar(!openSidebar);
  };
  const [selectedCountry, setSelectedCountry] = useState<Country | null>(null);

  return (
    <div className="bg-gray-100 min-h-screen flex container mx-auto ">
      {/* Sidebar / Drawer */}
      <div className={`w-15 h-screen ${isMobile ? 'hidden' : 'block'}`}>
        <MyDrawer
          open={openSidebar}
          onClose={() => setOpenSidebar(false)}
          isLoading={isLoading}
        />
      </div>

      <div className="flex-1 py-2 px-6">
        <TopBar
          onCountryChange={setSelectedCountry}
          onToggleSidebar={handleToggleSidebar}
          showHamburger={isMobile}
          isLoading={isLoading}
        />

       <div className="mt-3">
          <QueryClientProvider client={queryClient}>
            <TeamListTable />
          </QueryClientProvider>
        </div>
      </div>
    </div>
  );
}

export default Dashboard;
