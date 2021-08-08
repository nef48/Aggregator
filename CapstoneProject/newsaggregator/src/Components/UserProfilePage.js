import React from 'react';
import UserData from '../Classes/UserData';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import { Topic } from '../Classes/Topic';
import { IsListEmpty, IsNullOrUndefined } from '../Utilities/CommonUtilities';
import * as API from '../API/AggregatorAPI';
import ArticleCard from './ArticleCard';

interface IUserProfilePageProps {
    isOpen: Boolean;
    onClose: () => void;
    changeTopics: () => void;
    changePassword: () => void;
    selectedTopics: Topic[];
    user: UserData;
}

export default function UserProfilePage(props: IUserProfilePageProps) {
    const [isTopicOpen, setTopicOpen] = React.useState(false);
    const [selectedTopics, setSelectedTopics] = React.useState([]);
    const [allTopics, setAllTopics] = React.useState([]);
    const [favoriteArticles, setFavoriteArticles] = React.useState([]);
    const topicMap = {};

    React.useEffect(() => {
        setSelectedTopics(props.selectedTopics);
        GetFavoriteArticles();
    }, []);

    const GetFavoriteArticles = () => {
        API.GetFavoriteArticles(props.user.userID).then(response => {
            setFavoriteArticles(response);
        })
    }

    const handleChangeTopics = () => {
        //props.changeTopics();
        API.GetAllTopics().then(response => {
            setAllTopics(response);

            allTopics.forEach(item => {
                if (!IsListEmpty(selectedTopics) && selectedTopics.indexOf(item.topicName) >= 0) {
                    topicMap[item.topicName] = true;
                }
                else {
                    topicMap[item.topicName] = false;
                }
            });

            setTopicOpen(true);
        });
    }

    const renderSelectionButtons = () => {
        return(
            !IsNullOrUndefined(allTopics) ? allTopics.map(item => {
                return (<div style={{ margin: 10, display: "inline-block" }}>
                    <Button variant="contained"
                        onClick={() => {handleTopicClick(item.topicName)}}>{item.topicName}
                    </Button>
                </div>)
            }) : <div>No topics available to select.  Please try again later.</div>);
    }

    const handleTopicClick = (topicName: string) => {
        topicMap[topicName] = !topicMap[topicName];
      }

    const handleSaveTopics = () => {
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

        setTopicOpen(false);
    }

    const handleCloseTopicForm = () => {
        setTopicOpen(false);
    }

    const handleChangePassword = () => {
        props.changePassword();
    }

    const renderArticleList = () => {
        return(!IsNullOrUndefined(favoriteArticles) ? favoriteArticles.map(art => {
        return (<div style={{ display: "inline-block" }}><ArticleCard article={art} userID={props.user.userID} /></div>)
      }) : <div>No Articles available for this topic.</div>)
    }

    return (
        <div>
            <Dialog open={props.isOpen} onClose={props.onClose} aria-labelledby="form-dialog-title">
                <DialogTitle>User: {props.user.username}</DialogTitle>
                <DialogContent>
                    <div>
                        {renderArticleList()}
                    </div>
                </DialogContent>
                <DialogActions>
                    <Button color="primary" onClick={handleChangeTopics}>
                        Update Topics
                    </Button>
                    <Button color="secondary" onClick={handleChangePassword}>
                        Change Password
                    </Button>
                    <Button color="secondary" onClick={props.onClose}>
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
            <Dialog open={isTopicOpen} onClose={handleCloseTopicForm}>
                <DialogTitle>Update Topics</DialogTitle>
                <DialogContent>
                <div style={{ width: "1000", overflow: "hidden" }} >
                    {renderSelectionButtons()}
                </div>
                </DialogContent>
                <DialogActions>
                    <Button color="primary" onClick={handleSaveTopics}>
                        Save
                    </Button>
                    <Button color="secondary" onClick={handleCloseTopicForm}>
                        Cancel
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    )
}