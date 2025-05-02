import React, { useState } from 'react';
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Checkbox,
  Button,
  Box,
  Avatar,
  Typography,
  Collapse,
  IconButton,
  TextField,
  InputAdornment,
  TablePagination,
  Skeleton,
  TableFooter,
  useMediaQuery,
  useTheme,
} from '@mui/material';
import EditOutlinedIcon from '@mui/icons-material/EditOutlined';
import DeleteOutlineOutlinedIcon from '@mui/icons-material/DeleteOutlineOutlined';
import LocationOnOutlinedIcon from '@mui/icons-material/LocationOnOutlined';
import GroupOutlinedIcon from '@mui/icons-material/GroupOutlined';
import CakeOutlinedIcon from '@mui/icons-material/CakeOutlined';
import BusinessOutlinedIcon from '@mui/icons-material/BusinessOutlined';
import HomeOutlinedIcon from '@mui/icons-material/HomeOutlined';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import SearchIcon from '@mui/icons-material/Search';
import GroupIcon from '@mui/icons-material/Group';
import { useQuery } from '@tanstack/react-query';
import { TeamMember } from '../interfaces/TeamMember';
import { fetchTeamMembers } from '../services/api';

const TeamListTable = () => {
  const [selected, setSelected] = useState<number[]>([]);
  const [openRow, setOpenRow] = useState<number | null>(null);
  const [searchTerm, setSearchTerm] = useState('');
  const [page, setPage] = useState(1);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const totalPages = 10;

  const theme = useTheme();
  const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
  const isTablet = useMediaQuery(theme.breakpoints.between('sm', 'md'));

  const { data: teamMembers = [], isLoading, isError, error } = useQuery<TeamMember[], Error>({
    queryKey: ['teamMembers', page, rowsPerPage],
    queryFn: () => fetchTeamMembers(page, rowsPerPage),
  });

  const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.checked) {
      const newSelected = filteredTeamMembers.map((member) => member.id);
      setSelected(newSelected);
      return;
    }
    setSelected([]);
  };

  const handleClick = (id: number) => {
    const selectedIndex = selected.indexOf(id);
    let newSelected: number[] = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }

    setSelected(newSelected);
  };

  const handleCheckboxClick = (event: React.MouseEvent, id: number) => {
    event.stopPropagation();
    handleClick(id);
  };

  const handleArrowClick = (event: React.MouseEvent, memberId: number) => {
    event.stopPropagation();
    setOpenRow(openRow === memberId ? null : memberId);
  };

  const handleEditClick = () => {
    // Implement edit logic here
  };

  const handleDeleteClick = () => {
    // Implement delete logic here
  };

  const isSelected = (id: number) => selected.indexOf(id) !== -1;

  const filteredTeamMembers = teamMembers.filter((member) =>
    member.name.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const handleChangePage = (_event: unknown, newPage: number) => {
    setPage(newPage + 1);
    setSelected([]);
  };

  const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(1);
    setSelected([]);
  };

  if (isLoading) {
    if (isMobile) {
      return (
        <div>
          <Box display="flex" flexDirection="column" gap={2} mb={3}>
            <Skeleton variant="text" width={80} height={24} />
            <Skeleton variant="rectangular" width="100%" height={32} />
            <Skeleton variant="text" width={100} height={20} />
            <Box alignSelf="flex-end">
              <Skeleton variant="rectangular" width={100} height={32} />
            </Box>
          </Box>
          <Box display="flex" flexDirection="column" gap={2}>
            {[...Array(11)].map((_, index) => (
              <Box key={index} className="rounded-lg bg-white p-3">
                <Box display="flex" alignItems="center" gap={1} mb={1}>
                  <Skeleton variant="circular" width={24} height={24} />
                  <Skeleton variant="text" width={120} />
                </Box>
                <Skeleton variant="text" width={100} />
                <Skeleton variant="rectangular" sx={{ width: 50 }} height={20} mt={1} />
                <Box display="flex" gap={0.5} mt={1} justifyContent="center">
                  <Skeleton variant="rectangular" width={24} height={24} />
                  <Skeleton variant="rectangular" width={24} height={24} />
                </Box>
              </Box>
            ))}
            <Box alignSelf="flex-end" mt={2}>
              <Skeleton variant="rectangular" width={180} height={28} />
            </Box>
          </Box>
        </div>
      );
    }

    return (
      <div>
        <Box
          display="flex"
          flexDirection={{ xs: 'column', sm: 'row' }}
          justifyContent="space-between"
          alignItems={{ xs: 'flex-start', sm: 'center' }}
          gap={{ xs: 2, sm: 0 }}
          mb={3}
        >
          <Box display="flex" alignItems="center" gap={1.5} flexWrap="wrap">
            <Skeleton variant="text" width={80} height={24} />
            <Skeleton variant="rectangular" sx={{ width: { xs: '100%', sm: 256 } }} height={32} />
            <Skeleton variant="text" width={100} height={20} />
          </Box>
          <Skeleton variant="rectangular" width={100} height={32} />
        </Box>
        <TableContainer className="rounded-lg bg-white">
          <Table size="small">
            <TableHead>
              <TableRow sx={{ height: 36 }}>
                <TableCell sx={{ padding: '4px' }}>
                  <Skeleton variant="rectangular" width={32} height={32} />
                </TableCell>
                {['Name', 'Position', 'Department', 'Email', 'Phone', 'Status', 'Edit', ''].map(
                  (header, index) => (
                    <TableCell
                      key={index}
                      sx={{
                        padding: '4px',
                        textAlign: header === 'Edit' ? 'center' : 'left',
                        display:
                          (header === 'Department' || header === 'Email' || header === 'Phone') &&
                          isTablet
                            ? 'none'
                            : 'table-cell',
                      }}
                    >
                      <Skeleton variant="text" width={60} />
                    </TableCell>
                  )
                )}
              </TableRow>
            </TableHead>
            <TableBody>
              {[...Array(11)].map((_, rowIndex) => (
                <TableRow key={rowIndex} sx={{ height: 36 }}>
                  <TableCell sx={{ padding: '4px' }}>
                    <Skeleton variant="rectangular" width={32} height={32} />
                  </TableCell>
                  <TableCell sx={{ padding: '4px' }}>
                    <Box display="flex" alignItems="center">
                      <Skeleton variant="circular" width={24} height={24} sx={{ mr: 1 }} />
                      <Skeleton variant="text" width={80} />
                    </Box>
                  </TableCell>
                  <TableCell sx={{ padding: '4px' }}>
                    <Skeleton variant="text" width={80} />
                  </TableCell>
                  <TableCell sx={{ padding: '4px', display: isTablet ? 'none' : 'table-cell' }}>
                    <Skeleton variant="text" width={80} />
                  </TableCell>
                  <TableCell sx={{ padding: '4px', display: isTablet ? 'none' : 'table-cell' }}>
                    <Skeleton variant="text" width={120} />
                  </TableCell>
                  <TableCell sx={{ padding: '4px', display: isTablet ? 'none' : 'table-cell' }}>
                    <Skeleton variant="text" width={80} />
                  </TableCell>
                  <TableCell sx={{ padding: '4px' }}>
                    <Skeleton variant="rectangular" width={50} height={20} />
                  </TableCell>
                  <TableCell sx={{ padding: '4px', textAlign: 'center' }}>
                    <Box display="flex" gap={0.5} justifyContent="center">
                      <Skeleton variant="rectangular" width={24} height={24} />
                      <Skeleton variant="rectangular" width={24} height={24} />
                    </Box>
                  </TableCell>
                  <TableCell sx={{ padding: '4px' }}>
                    <Skeleton variant="circular" width={20} height={20} />
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
            <TableFooter>
              <TableRow sx={{ height: 28 }}>
                <TableCell colSpan={9} align="right" sx={{ padding: '2px' }}>
                  <Skeleton variant="rectangular" width={180} height={28} />
                </TableCell>
              </TableRow>
            </TableFooter>
          </Table>
        </TableContainer>
      </div>
    );
  }

  if (isError) {
    return <Typography variant="body2">Error: {error.message}</Typography>;
  }

  if (isMobile) {
    return (
      <div>
        <Box display="flex" flexDirection="column" gap={2} mb={1}>
          <TextField
            variant="outlined"
            placeholder="Search by name, email..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            size="small"
            fullWidth
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <SearchIcon className="text-gray-500" fontSize="small" />
                </InputAdornment>
              ),
            }}
          />
          <Box display="flex" alignItems="center" gap={0.5}>
            <Checkbox
              color="primary"
              size="small"
              indeterminate={selected.length > 0 && selected.length < filteredTeamMembers.length}
              checked={filteredTeamMembers.length > 0 && selected.length === filteredTeamMembers.length}
              onChange={handleSelectAllClick}
            />
            <GroupIcon className="text-gray-500" fontSize="small" />
            <Typography variant="caption" className="text-gray-500">
              {selected.length} Selected
            </Typography>
          </Box>
          <Button
            variant="contained"
            size="small"
            className="bg-blue-700 text-white capitalize rounded-lg px-3 py-1"
            sx={{ alignSelf: 'flex-end' }}
          >
            Add User
          </Button>
        </Box>
        <Box display="flex" flexDirection="column" gap={2}>
          {filteredTeamMembers.map((member) => {
            const isItemSelected = isSelected(member.id);
            return (
              <Box
                key={member.id}
                className="rounded-lg bg-white p-3"
                onClick={() => handleClick(member.id)}
                sx={{ border: isItemSelected ? '1px solid #1976d2' : 'none' }}
              >
                <Box display="flex" alignItems="center" gap={1} mb={1}>
                  <Checkbox
                    color="primary"
                    size="small"
                    checked={isItemSelected}
                    onClick={(event) => handleCheckboxClick(event, member.id)}
                  />
                  <Avatar src={member.avatar} className="w-6 h-6" />
                  <Typography variant="body2">{member.name}</Typography>
                </Box>
                <Typography variant="caption" color="textSecondary">
                  {member.position}
                </Typography>
                <Box
                  className={`inline-block rounded-md px-1.5 py-0.5 text-xs mt-1 ${
                    member.status === 'Full Time'
                      ? 'bg-green-100 text-green-700'
                      : 'bg-orange-100 text-orange-700'
                  }`}
                >
                  <Typography variant="caption">{member.status}</Typography>
                </Box>
                <Box display="flex" gap={0.5} mt={1} justifyContent="center">
                  <Button
                    variant="outlined"
                    size="small"
                    className="border-gray-200 rounded-md min-w-[24px] p-0.5"
                    onClick={handleEditClick}
                  >
                    <EditOutlinedIcon fontSize="small" className="text-gray-600" />
                  </Button>
                  <Button
                    variant="outlined"
                    size="small"
                    className="border-gray-200 rounded-md min-w-[24px] p-0.5"
                    onClick={handleDeleteClick}
                  >
                    <DeleteOutlineOutlinedIcon fontSize="small" className="text-gray-600" />
                  </Button>
                  <IconButton
                    aria-label="expand row"
                    size="small"
                    onClick={(event) => handleArrowClick(event, member.id)}
                    sx={{ padding: '2px' }}
                  >
                    {openRow === member.id ? (
                      <KeyboardArrowUpIcon fontSize="small" />
                    ) : (
                      <KeyboardArrowDownIcon fontSize="small" />
                    )}
                  </IconButton>
                </Box>
                <Collapse in={openRow === member.id} timeout="auto" unmountOnExit>
                  <Box mt={2} p={1} border={1} borderColor="grey.200" borderRadius={1}>
                    <Box display="flex" flexDirection="column" gap={1}>
                      <Box display="flex" gap={1}>
                        <LocationOnOutlinedIcon className="text-gray-600" fontSize="small" />
                        <Typography variant="caption">Office: {member.address}</Typography>
                      </Box>
                      <Box display="flex" gap={1}>
                        <GroupOutlinedIcon className="text-gray-600" fontSize="small" />
                        <Typography variant="caption">Team: {member.teamMates}</Typography>
                      </Box>
                      <Box display="flex" gap={1}>
                        <CakeOutlinedIcon className="text-gray-600" fontSize="small" />
                        <Typography variant="caption">Birthday: {member.birthday}</Typography>
                      </Box>
                      <Box display="flex" gap={1}>
                        <BusinessOutlinedIcon className="text-gray-600" fontSize="small" />
                        <Typography variant="caption">HR Years: {member.hrYears} Years</Typography>
                      </Box>
                      <Box display="flex" gap={1}>
                        <HomeOutlinedIcon className="text-gray-600" fontSize="small" />
                        <Typography variant="caption">Address: {member.address}</Typography>
                      </Box>
                      <Box display="flex" gap={1}>
                        <Typography variant="caption">Email: {member.email}</Typography>
                      </Box>
                      <Box display="flex" gap={1}>
                        <Typography variant="caption">Phone: {member.phone}</Typography>
                      </Box>
                    </Box>
                  </Box>
                </Collapse>
              </Box>
            );
          })}
          <TablePagination
            rowsPerPageOptions={[]}
            component="div"
            count={totalPages * rowsPerPage}
            rowsPerPage={rowsPerPage}
            page={page - 1}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleChangeRowsPerPage}
            size="small"
            sx={{ fontSize: '0.7rem', padding: '2px', mt: 2 }}
            showFirstButton={false}
            showLastButton={false}
          />
        </Box>
      </div>
    );
  }

  return (
    <div>
      <Box
        display="flex"
        flexDirection={{ xs: 'column', sm: 'row' }}
        justifyContent="space-between"
        alignItems={{ xs: 'flex-start', sm: 'center' }}
        gap={{ xs: 2, sm: 0 }}
        mb={1.5}
      >
        <Box display="flex" alignItems="center" gap={1.5} flexWrap="wrap">
          <TextField
            variant="outlined"
            placeholder="Search by name, email..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            size="small"
            sx={{ width: { xs: '100%', sm: 256 } }}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <SearchIcon className="text-gray-500" fontSize="small" />
                </InputAdornment>
              ),
            }}
          />
          <Box display="flex" alignItems="center" gap={0.5}>
            <Checkbox
              color="primary"
              size="small"
              indeterminate={selected.length > 0 && selected.length < filteredTeamMembers.length}
              checked={filteredTeamMembers.length > 0 && selected.length === filteredTeamMembers.length}
              onChange={handleSelectAllClick}
            />
            <GroupIcon className="text-gray-500" fontSize="small" />
            <Typography variant="caption" className="text-gray-500">
              {selected.length} Selected
            </Typography>
          </Box>
        </Box>
        <Button
          variant="contained"
          size="small"
          className="bg-blue-700 text-white capitalize rounded-lg px-3 py-1"
        >
          Add User
        </Button>
      </Box>
      <TableContainer className="rounded-lg bg-white">
        <Table size="small">
          <TableHead>
            <TableRow sx={{ height: 36 }}>
              <TableCell sx={{ padding: '4px' }} />
              <TableCell sx={{ padding: '4px' }}>
                <Typography variant="body2" sx={{ fontWeight: 800, color: '#000000' }}>
                  Name
                </Typography>
              </TableCell>
              <TableCell sx={{ padding: '4px' }}>
                <Typography variant="body2" sx={{ fontWeight: 800, color: '#000000' }}>
                  Position
                </Typography>
              </TableCell>
              <TableCell sx={{ padding: '4px', display: { xs: 'none', md: 'table-cell' } }}>
                <Typography variant="body2" sx={{ fontWeight: 800, color: '#000000' }}>
                  Department
                </Typography>
              </TableCell>
              <TableCell sx={{ padding: '4px', display: { xs: 'none', md: 'table-cell' } }}>
                <Typography variant="body2" sx={{ fontWeight: 800, color: '#000000' }}>
                  Email
                </Typography>
              </TableCell>
              <TableCell sx={{ padding: '4px', display: { xs: 'none', md: 'table-cell' } }}>
                <Typography variant="body2" sx={{ fontWeight: 800, color: '#000000' }}>
                  Phone
                </Typography>
              </TableCell>
              <TableCell sx={{ padding: '4px' }}>
                <Typography variant="body2" sx={{ fontWeight: 800, color: '#000000' }}>
                  Status
                </Typography>
              </TableCell>
              <TableCell sx={{ padding: '4px', textAlign: 'center' }}>
                <Typography variant="body2" sx={{ fontWeight: 800, color: '#000000' }}>
                  Edit
                </Typography>
              </TableCell>
              <TableCell sx={{ padding: '4px' }} />
            </TableRow>
          </TableHead>
          <TableBody>
            {filteredTeamMembers.map((member) => {
              const isItemSelected = isSelected(member.id);
              return (
                <React.Fragment key={member.id}>
                  <TableRow
                    hover
                    onClick={() => handleClick(member.id)}
                    role="checkbox"
                    aria-checked={isItemSelected}
                    selected={isItemSelected}
                    className="hover:bg-gray-50"
                    sx={{ height: 36 }}
                  >
                    <TableCell padding="checkbox" sx={{ padding: '4px' }}>
                      <Checkbox
                        color="primary"
                        size="small"
                        checked={isItemSelected}
                        onClick={(event) => handleCheckboxClick(event, member.id)}
                      />
                    </TableCell>
                    <TableCell sx={{ padding: '4px' }}>
                      <Box className="flex items-center">
                        <Avatar src={member.avatar} className="w-6 h-6 mr-1" />
                        <Typography variant="body2">{member.name}</Typography>
                      </Box>
                    </TableCell>
                    <TableCell sx={{ padding: '4px' }}>
                      <Typography variant="body2">{member.position}</Typography>
                    </TableCell>
                    <TableCell sx={{ padding: '4px', display: { xs: 'none', md: 'table-cell' } }}>
                      <Typography variant="body2">{member.department}</Typography>
                    </TableCell>
                    <TableCell sx={{ padding: '4px', display: { xs: 'none', md: 'table-cell' } }}>
                      <Typography variant="body2">{member.email}</Typography>
                    </TableCell>
                    <TableCell sx={{ padding: '4px', display: { xs: 'none', md: 'table-cell' } }}>
                      <Typography variant="body2">{member.phone}</Typography>
                    </TableCell>
                    <TableCell sx={{ padding: '4px' }}>
                      <Box
                        className={`inline-block rounded-md px-1.5 py-0.5 text-xs ${
                          member.status === 'Full Time'
                            ? 'bg-green-100 text-green-700'
                            : 'bg-orange-100 text-orange-700'
                        }`}
                      >
                        <Typography variant="caption">{member.status}</Typography>
                      </Box>
                    </TableCell>
                    <TableCell sx={{ padding: '4px', textAlign: 'center' }}>
                      <Box className="flex gap-0.5" sx={{ justifyContent: 'center' }}>
                        <Button
                          variant="outlined"
                          size="small"
                          className="border-gray-200 rounded-md min-w-[24px] p-0.5"
                          onClick={handleEditClick}
                        >
                          <EditOutlinedIcon fontSize="small" className="text-gray-600" />
                        </Button>
                        <Button
                          variant="outlined"
                          size="small"
                          className="border-gray-200 rounded-md min-w-[24px] p-0.5"
                          onClick={handleDeleteClick}
                        >
                          <DeleteOutlineOutlinedIcon fontSize="small" className="text-gray-600" />
                        </Button>
                      </Box>
                    </TableCell>
                    <TableCell sx={{ padding: '4px' }}>
                      <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={(event) => handleArrowClick(event, member.id)}
                        sx={{ padding: '2px' }}
                      >
                        {openRow === member.id ? (
                          <KeyboardArrowUpIcon fontSize="small" />
                        ) : (
                          <KeyboardArrowDownIcon fontSize="small" />
                        )}
                      </IconButton>
                    </TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={9}>
                      <Collapse in={openRow === member.id} timeout="auto" unmountOnExit>
                        <Box className="m-1 border border-gray-200 rounded-md p-1.5">
                          <div className="flex gap-1.5 mb-1.5">
                            <div className="flex-1 flex items-center gap-0.5">
                              <LocationOnOutlinedIcon className="text-gray-600" fontSize="small" />
                              <Typography variant="caption">OFFICE LOCATION</Typography>
                            </div>
                            <div className="flex-1">
                              <Typography variant="caption" className="text-gray-600">
                                {member.address}
                              </Typography>
                            </div>
                          </div>
                          <div className="flex gap-1.5 mb-1.5">
                            <div className="flex-1 flex items-center gap-0.5">
                              <GroupOutlinedIcon className="text-gray-600" fontSize="small" />
                              <Typography variant="caption">TEAM MATES</Typography>
                            </div>
                            <div className="flex-1">
                              <Typography variant="caption" className="text-gray-600">
                                {member.teamMates}
                              </Typography>
                            </div>
                          </div>
                          <div className="flex gap-1.5 mb-1.5">
                            <div className="flex-1 flex items-center gap-0.5">
                              <CakeOutlinedIcon className="text-gray-600" fontSize="small" />
                              <Typography variant="caption">BIRTHDAY</Typography>
                            </div>
                            <div className="flex-1">
                              <Typography variant="caption" className="text-gray-600">
                                {member.birthday}
                              </Typography>
                            </div>
                          </div>
                          <div className="flex gap-1.5 mb-1.5">
                            <div className="flex-1 flex items-center gap-0.5">
                              <BusinessOutlinedIcon className="text-gray-600" fontSize="small" />
                              <Typography variant="caption">HR YEARS</Typography>
                            </div>
                            <div className="flex-1">
                              <Typography variant="caption" className="text-gray-600">
                                {member.hrYears} Years
                              </Typography>
                            </div>
                          </div>
                          <div className="flex gap-1.5">
                            <div className="flex-1 flex items-center gap-0.5">
                              <HomeOutlinedIcon className="text-gray-600" fontSize="small" />
                              <Typography variant="caption">ADDRESS</Typography>
                            </div>
                            <div className="flex-1">
                              <Typography variant="caption" className="text-gray-600">
                                {member.address}
                              </Typography>
                            </div>
                          </div>
                        </Box>
                      </Collapse>
                    </TableCell>
                  </TableRow>
                </React.Fragment>
              );
            })}
          </TableBody>
          <TableFooter>
            <TableRow sx={{ height: 28 }}>
              <TableCell colSpan={9} align="right" sx={{ padding: '2px' }}>
                <TablePagination
                  rowsPerPageOptions={isTablet ? [] : [5, 10, 25]}
                  component="div"
                  count={totalPages * rowsPerPage}
                  rowsPerPage={rowsPerPage}
                  page={page - 1}
                  onPageChange={handleChangePage}
                  onRowsPerPageChange={handleChangeRowsPerPage}
                  size="small"
                  sx={{ fontSize: '0.7rem', padding: '2px' }}
                  showFirstButton={isTablet ? false : true}
                  showLastButton={isTablet ? false : true}
                />
              </TableCell>
            </TableRow>
          </TableFooter>
        </Table>
      </TableContainer>
    </div>
  );
};

export default TeamListTable;