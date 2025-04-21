import { useState } from 'react';
import {
  Box,
  Button,
  Checkbox,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  TextField,
  Paper,
  IconButton,
  Typography,
  Stack
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import { CheckCircleOutline,} from '@mui/icons-material';

export default function TodoList() {
  type Task = {
    text: string;
    completed: boolean;
  };
  const [tasks, setTasks] = useState<Task[]>([]);
 
  const [newTask, setNewTask] = useState('');

  const addTask = () => {
    if (newTask.trim()) {
      setTasks([...tasks, { text: newTask, completed: false }]);
      setNewTask('');
    }
  };

  const toggleTask = (index: number) => {
    const updatedTasks = tasks.map((task, i) =>
      i === index ? { ...task, completed: !task.completed } : task
    );
    setTasks(updatedTasks);
  };

  const deleteTask = (index: number) => {
    setTasks(tasks.filter((_, i) => i !== index));
  };

  return (
    <>


      <Box sx={{ maxWidth: 600, margin: 'auto', p: 2 }}>
        <Typography variant="h4" sx={{ color:'black'}}>
          Todo List
        </Typography>
        <Paper elevation={3} sx={{ p: 2 }}>
          <Stack direction="row" spacing={2} alignItems="center">
            <TextField
              fullWidth
              size='small'
              label="Add a new task"
              value={newTask}
              onChange={(e) => setNewTask(e.target.value)}
              onKeyPress={(e) => e.key === 'Enter' && addTask()}
            />
            <Button
              variant="contained"
              size='medium'
              onClick={addTask}
              disabled={!newTask.trim()}
              sx={{ height: '100%' }}
            >
              Add
            </Button>

          </Stack>

          <List>
            {tasks.map((task, index) => (
              <ListItem
                key={index}
                secondaryAction={
                  <IconButton edge="end" onClick={() => deleteTask(index)}>
                    <DeleteIcon />
                  </IconButton>
                }
                disablePadding
              >
                <ListItemButton onClick={() => toggleTask(index)}>
                  <ListItemText
                    primary={task.text}
                    sx={{
                      textDecoration: task.completed ? 'line-through' : 'none',
                      pr: 2 
                    }}
                  />
                  <Checkbox
                    edge="end"
                    icon={<CheckCircleOutline />}
                    checkedIcon={<CheckCircleOutline color="success" />}
                    checked={task.completed}
                    sx={{ ml: 'auto' }} 
                  />
                </ListItemButton>
              </ListItem>
            ))}
          </List>
        </Paper>
      </Box>
    </>
  );
}