import React from 'react';
import { fade, makeStyles, Theme, createStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import * as API from '../API/AggregatorAPI';
import Typography from '@material-ui/core/Typography';
import HomePage from './HomePage';
import TopicButton from './TopicButton';
import { Topic } from '../Classes/Topic';
import { IsListEmpty, IsNullOrUndefined } from '../Utilities/CommonUtilities';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import { UserData } from '../Classes/UserData';

interface ITopicSelectPageProps {
  user: UserData;
  selectedTopics?: string[];
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
    topicNotSelected: {

    },
    topicSelected: {
        
    }
  }),
);

export default function TopicSelectPage(props: ITopicSelectPageProps) {
    const classes = useStyles();
    const [areTopicsSelected, setAreTopicsSelected] = React.useState(false);
    const [selectedTopics, setSelectedTopics] = React.useState([]);
    const [allTopics, setAllTopics] = React.useState([]);
    //const [topicNames, setTopicNames] = React.useState("");
    const [updated, setUpdated] = React.useState(false);
    const topicMap = {};
    let topicNames = "";

    React.useEffect(() => {
      getAllTopics();
    }, []);

    const getAllTopics = () => {
      API.GetAllTopics().then(response => {
        setAllTopics(response);

        allTopics.forEach(item => {
          if (!IsListEmpty(props.selectedTopics) && props.selectedTopics.indexOf(item.topicName) >= 0) {
            topicMap[item.topicName] = true;
          }
          else {
            topicMap[item.topicName] = false;
          }
        });
      });
    }

    const applySelectedTopics = () => {
        let tempTopics: Topic[] = [];

        Object.entries(topicMap).map(([key,value]) => {
            if (value === true) {
                let temp: Topic = new Topic();
                temp.TopicName = key;
                tempTopics.push(temp);
            }
        });

        setSelectedTopics(tempTopics);

        tempTopics.map(item => {
          console.dir(item);
          let topicID = allTopics.find(x => x.topicName == item.TopicName).topicID;
          API.AddTopicToUser(topicID, props.user.userID);
        });

        setAreTopicsSelected(true);
    }

    const handleTopicClick = (topicName: string) => {
      topicMap[topicName] = !topicMap[topicName];

      topicNames += (topicName + '\n');

      setUpdated(true);
      //setTopicNames(tempTopicNames);
    }

    const renderSelectionButtons = () => {
        return(
            !IsNullOrUndefined(allTopics) ? allTopics.map(item => {
                return (<div style={{ margin: 10, display: "inline-block" }}>
                    <Button variant="contained"
                        onClick={() => {handleTopicClick(item.topicName)}}>{item.topicName}
                    </Button>
                    {/* <TopicButton onClick={handleTopicClick} topicName={item.topicName} isSelected={topicMap[item.topicName]} /> */}
                </div>)
            }) : <div>No topics available to select.  Please try again later.</div>);
    }

    return(
        <div>
            {!areTopicsSelected &&
            <div>
                {IsListEmpty(props.selectedTopics) &&<React.Fragment>
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
                            <div className={classes.grow} />
                            <Typography className={classes.title} variant="h6" noWrap>
                                Select One or More Topics to Continue
                            </Typography>
                        </Toolbar>
                    </AppBar>
                </React.Fragment>}
                <React.Fragment>
                  <div style={{ marginTop: 75 }}>
                      <div style={{ width: "calc(100vw)", overflow: "hidden" }} >
                          {renderSelectionButtons()}
                      </div>
                      <div style={{ marginTop: 75 }}>
                          <div style={{ display: "inline-block" }}>
                              <TextField
                                  label="Selected Topics"
                                  InputProps={{
                                      readOnly: true
                                  }}
                                  multiline
                                  rows={allTopics.length}
                                  value={topicNames}
                                  variant="outlined"
                              />
                          </div>
                          <div style={{ marginLeft: 50, display: "inline-block" }}>
                              <Button onClick={() => {applySelectedTopics()}} 
                                  color="primary" 
                                  variant="contained">Apply Topics</Button>
                          </div>
                      </div>
                  </div>
              </React.Fragment>
            </div>}
            {areTopicsSelected &&
            <React.Fragment>
                <HomePage selectedTopics={selectedTopics} user={props.user} />
            </React.Fragment>}
        </div>
    );
}