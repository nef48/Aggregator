import * as React from 'react';
import { fade, makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import Typography from '@material-ui/core/Typography';
import SearchIcon from '@material-ui/icons/Search';
import AccountCircle from '@material-ui/icons/AccountCircle';
import InputBase from '@material-ui/core/InputBase';
import Button from '@material-ui/core/Button';
import ButtonGroup from '@material-ui/core/ButtonGroup';
import * as API from '../API/AggregatorAPI';
import { Topic } from '../Classes/Topic';
import { IsNullOrUndefined } from '../Utilities/CommonUtilities';
import ArticleCard from './ArticleCard';
import UserProfilePage from './UserProfilePage';
import { UserData } from '../Classes/UserData';
import LoginPage from './LoginPage';

interface IHomePageProps {
  selectedTopics: Topic[];
  user: UserData;
}

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

export default function HomePage(props: IHomePageProps) {
    const classes = useStyles();
    const menuId = 'primary-search-account-menu';
    const [selectedTopic, setSelectedTopic] = React.useState(null);
    const [articles, setArticles] = React.useState([]);
    const [isUserProfileOpen, setUserProfileOpen] = React.useState(false);

    React.useEffect(() => {
      let firstTopic = props.selectedTopics[0].TopicName;
      handleTopicSelection(firstTopic);
    }, []);

    const handleProfileMenuOpen = (event) => {
      setUserProfileOpen(true);
    };

    const handleProfilePageClose = () => {
      setUserProfileOpen(false);
    }

    const handleLogOff = () => {

    }

    const handleChangeTopics = () => {

    }

    const handleTopicSelection = (topic: string) => {
      let selectedTopic: Topic = props.selectedTopics.find(x => x.TopicName === topic);
      setSelectedTopic(selectedTopic);
      API.GetNewsFeed(topic).then(response => {
        setArticles(response);
      });
    }

    let topicList = !IsNullOrUndefined(props.selectedTopics) ? props.selectedTopics.map(item => {
      return (<Button onClick={() => {handleTopicSelection(item.TopicName)}}>{item.TopicName}</Button>)
    }) : <div>Please Select a list of Topics to continue</div>

    let articleList = !IsNullOrUndefined(articles) ? articles.map(art => {
      return (<div style={{ display: "inline-block" }}><ArticleCard article={art} /></div>)
    }) : <div>No Articles available for this topic.</div>

    return (
        <div>
          <React.Fragment>
              <AppBar position="fixed">
                  <Toolbar>
                      <IconButton
                          edge="start"
                          className={classes.menuButton}
                          color="inherit"
                          aria-label="open drawer">
                          <MenuIcon />
                      </IconButton>
                      <Typography className={classes.title} variant="h6" noWrap>
                          News Aggregator
                      </Typography>
                      <div className={classes.search}>
                          <div className={classes.searchIcon}>
                              <SearchIcon />
                          </div>
                          <InputBase
                              placeholder="Search…"
                              classes={{
                                  root: classes.inputRoot,
                                  input: classes.inputInput,
                              }}
                              inputProps={{ 'aria-label': 'search' }}
                          />
                      </div>
                      <div className={classes.grow} />
                      <div className={classes.sectionDesktop}>
                          <IconButton
                              edge="end"
                              aria-label="account of current user"
                              aria-controls={menuId}
                              aria-haspopup="true"
                              onClick={handleProfileMenuOpen}
                              color="inherit">
                              <AccountCircle />
                          </IconButton>
                      </div>
                  </Toolbar>
              </AppBar>
          </React.Fragment>
          <React.Fragment>
            <div style={{ marginTop: 75 }}>
              <ButtonGroup variant="text" color="primary" aria-label="text primary button group">
                {topicList}
              </ButtonGroup>
            </div>
          </React.Fragment>
          <React.Fragment>
            <div style={{ width: "calc(100vw)", overflow: "hidden" }}>
              {articleList}
            </div>
          </React.Fragment>
          <UserProfilePage 
            isOpen={isUserProfileOpen} 
            onClose={handleProfilePageClose} 
            user={props.user} 
            changeTopics={handleChangeTopics}
            logOff={handleLogOff} />
        </div>
    );
}