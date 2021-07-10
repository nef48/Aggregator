import React from 'react';
import * as moment from 'moment';
import { makeStyles } from '@material-ui/core/styles';
import clsx from 'clsx';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardMedia from '@material-ui/core/CardMedia';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Collapse from '@material-ui/core/Collapse';
import Avatar from '@material-ui/core/Avatar';
import IconButton from '@material-ui/core/IconButton';
import Typography from '@material-ui/core/Typography';
import { red } from '@material-ui/core/colors';
import FavoriteIcon from '@material-ui/icons/Favorite';
import ShareIcon from '@material-ui/icons/Share';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import { Article } from '../Classes/Article';

interface IArticleCardProps {
    article: Article
}

const useStyles = makeStyles((theme) => ({
  root: {
    maxWidth: 345,
  },
  media: {
    height: 0,
    paddingTop: '56.25%', // 16:9
  },
  expand: {
    transform: 'rotate(0deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
      duration: theme.transitions.duration.shortest,
    }),
  },
  expandOpen: {
    transform: 'rotate(180deg)',
  },
}));

export default function ArticleCard(props: IArticleCardProps) {
  const classes = useStyles();
  const [expanded, setExpanded] = React.useState(false);

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  let dateString = moment(props.article.DatePublished).format("MM/dd/YYYY HH:mm");

  return (
    <div>
      <Card className={classes.root}>
        <CardHeader title={props.article.ArticleTitle} subheader={props.article.ArticleAuthor + " " + dateString}/>
        <CardMedia className={classes.media} title={props.article.ArticleTitle}/>
        <CardContent>
          <Typography variant="body2" color="textSecondary" component="p">
            {props.article.ArticleDescription}
          </Typography>
        </CardContent>
        <CardActions disableSpacing>
          <IconButton aria-label="add to favorites">
            <FavoriteIcon />
          </IconButton>
        </CardActions>
        <Collapse in={expanded} timeout="auto" unmountOnExit>
          <CardContent>
            <Typography paragraph>Additional Information:</Typography>
            <Typography paragraph>{props.article.AdditionalDescription}</Typography>
          </CardContent>
        </Collapse>
      </Card>
    </div>
  );
}