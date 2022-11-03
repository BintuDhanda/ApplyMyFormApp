import React, {useContext, useEffect, useState} from 'react';
import {View, Text, StyleSheet, ScrollView} from 'react-native';
import {CFetch} from '../../settings/APIFetch';
import {AppContext} from '../../settings/Context';
import Loader from '../../utilities/Loader';

const ApplicationDetail = ({route}) => {
  const {appUser} = useContext(AppContext);
  const [loading, setLoading] = useState(true);
  const [waiting, setWaiting] = useState(false);
  const [item, setItem] = useState('');

  const loadData = () => {
    CFetch('ApplicationDetail', appUser.token, {
      id: route.params.id,
    })
      .then(res => {
        if (res.status === 200) {
          res.json().then(result => {
            setItem(result);
          });
        }
      })
      .catch(error => {
        console.log(error);
      })
      .finally(() => {
        setLoading(false);
        setWaiting(false);
      });
  };

  useEffect(() => {
    loadData();
  }, []);

  return (
    <>
      <Loader loading={loading} waiting={waiting} spinner={true} />
      {item != '' && (
        <ScrollView>
          <View style={styles.container}>
            <Text>{item.job.jobName}</Text>
          </View>
        </ScrollView>
      )}
    </>
  );
};

export default ApplicationDetail;

const styles = StyleSheet.create({
  container: {},
});
