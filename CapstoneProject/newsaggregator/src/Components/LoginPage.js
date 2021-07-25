import React from 'react';
import { fade, makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import TopicSelectPage from './TopicSelectPage';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import SignupPage from './SignupPage';

const useStyles = makeStyles((theme) =>
  createStyles({
    grow: {
      flexGrow: 1,
    },
    menuButton: {
      marginRight: theme.spacing(2),
    },
    title: {
      display: 'none',
      [theme.breakpoints.up('sm')]: {
        display: 'block',
      },
    },
    search: {
      position: 'relative',
      borderRadius: theme.shape.borderRadius,
      backgroundColor: fade(theme.palette.common.white, 0.15),
      '&:hover': {
        backgroundColor: fade(theme.palette.common.white, 0.25),
      },
      marginRight: theme.spacing(2),
      marginLeft: 0,
      width: '100%',
      [theme.breakpoints.up('sm')]: {
        marginLeft: theme.spacing(3),
        width: 'auto',
      },
    },
    searchIcon: {
      padding: theme.spacing(0, 2),
      height: '100%',
      position: 'absolute',
      pointerEvents: 'none',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
    },
    inputRoot: {
      color: 'inherit',
    },
    inputInput: {
      padding: theme.spacing(1, 1, 1, 0),
      // vertical padding + font size from searchIcon
      paddingLeft: `calc(1em + ${theme.spacing(4)}px)`,
      transition: theme.transitions.create('width'),
      width: '100%',
      [theme.breakpoints.up('md')]: {
        width: '20ch',
      },
    },
    sectionDesktop: {
      display: 'none',
      [theme.breakpoints.up('md')]: {
        display: 'flex',
      },
    },
    sectionMobile: {
      display: 'flex',
      [theme.breakpoints.up('md')]: {
        display: 'none',
      },
    },
  }),
);

export default function LoginPage() {
    const classes = useStyles();
    const [signupOpen, setSignupOpen] = React.useState(false);
    const [username, setUsername] = React.useState("");
    const [password, setPassword] = React.useState("");

    const handleOpenSignup = () => {
        setSignupOpen(true);
    }
    
    const handleCloseSignup = () => {
        setSignupOpen(false);
    }

    return(
        <div>
            <React.Fragment>
                <AppBar position="fixed">
                    <Toolbar>
                        <Typography className={classes.title} variant="h6" noWrap>
                            News Aggregator
                        </Typography>
                        <div className={classes.grow} />
                        <Button onClick={handleOpenSignup} color="primary" disableElevation variant="contained">Signup</Button>
                    </Toolbar>
                </AppBar>
            </React.Fragment>
            <div className="center-screen">
                <div>
                    <TextField 
                        label="Username" 
                        variant="outlined" 
                        value={username}
                        style={{ width: 500 }} />
                </div>
                <div style={{ marginTop: 10 }}>
                    <TextField 
                        label="Password" 
                        variant="outlined" 
                        type="password" 
                        value={password}
                        style={{ width: 500 }} />
                </div>
                <div style={{ marginTop: 10 }}>
                    <Button 
                        variant="contained" 
                        color="primary"
                        style={{ width: 500 }}>Login</Button>
                </div>
            </div>
            <SignupPage isOpen={signupOpen} onClose={handleCloseSignup} />
        </div>
    );
}