import {StyleSheet, View, Button, Alert} from 'react-native';
import React, {useContext, useState} from 'react';
import {CFetch} from '../settings/APIFetch';
import {AppContext} from '../settings/Context';
import Loader from '../utilities/Loader';
const ComingSoon = ({route}) => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(false);
  const [waiting, setWaiting] = useState(false);
  const [clicked, setClicked] = useState(false);
  const onSelectComingSoonService = () => {
    if (!route.params) {
      return faslse;
    }
    setLoading(true);
    setWaiting(true);
    setClicked(true);
    CFetch('ServiceNotification', appUser.token, {
      Services: route.params.name,
    })
      .then(res => {
        if (res.status === 441) {
          res.json().then(result => alert(result));
        } else if (res.status === 200) {
          alert('Applied Succesfully');
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setTimeout(() => {
          setLoading(false);
          setWaiting(false);
        });
      });
  };

  return (
    <>
      {<Loader loading={loading} waiting={waiting} spinner={true} />}
      <View style={styles.container}>
        <Button
          disabled={clicked}
          title="Click to Apply"
          color="#4CA6A8"
          onPress={() => onSelectComingSoonService()}
        />
      </View>
    </>
  );
};

export default ComingSoon;
const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});
