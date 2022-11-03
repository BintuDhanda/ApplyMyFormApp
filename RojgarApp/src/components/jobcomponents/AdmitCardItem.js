import {StyleSheet, Text, Touchable, View} from 'react-native';
import React from 'react';
import FontAwesome from 'react-native-vector-icons/dist/FontAwesome';
import {TouchableRipple} from 'react-native-paper';

const AdmitCardItem = ({item, index, handlDownloadAdmitCard}) => {
  return (
    <View key={index} style={styles.container}>
      <View style={styles.jobcard}>
        <Text style={styles.category}>{item.job.category.name}</Text>
        <Text style={styles.jobtitle}>{item.job.jobName}</Text>
        <Text style={styles.jobdesc}>{item.job.jobDepartment}</Text>
        <View style={styles.lastrow}>
          <View style={styles.statusrow}>
            <Text>
              <FontAwesome style={styles.icon} name="calendar" />{' '}
              {new Date(item.updatedOn).toDateString()}
            </Text>
          </View>
          <TouchableRipple
            onPress={() => handlDownloadAdmitCard(item.admitCardUrl)}>
            <View style={styles.statusrow}>
              <Text style={styles.statustext}>Download</Text>
            </View>
          </TouchableRipple>
        </View>
      </View>
    </View>
  );
};

export default AdmitCardItem;

const styles = StyleSheet.create({
  container: {
    // paddingHorizontal: 10,
  },
  jobcard: {
    paddingLeft: 20,
    padding: 15,
    backgroundColor: 'white',
    borderRadius: 5,
  },
  category: {
    fontSize: 13,
    fontWeight: '700',
    color: '#4CA6A8',
  },
  jobtitle: {
    paddingTop: 2,
    paddingBottom: 2,
    fontSize: 20,
    fontWeight: '700',
    color: 'black',
  },
  jobdesc: {
    paddingTop: 2,
    paddingBottom: 2,
    fontSize: 14,
    fontWeight: '500',
  },
  lastrow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    paddingTop: 12,
  },
  statusrow: {
    backgroundColor: '#dcefef',
    padding: 7,
    paddingLeft: 12,
    paddingRight: 12,
    borderRadius: 25,
    alignItems: 'center',
  },
  statustext: {
    fontSize: 13,
    fontWeight: '600',
  },
  icon: {
    fontSize: 15,
    color: '#4CA6A8',
  },
});
