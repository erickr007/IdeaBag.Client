//
//  SettingsNavigationViewController.m
//  IdeaBag-3
//
//  Created by Joe Petraglia on 9/21/15.
//  Copyright (c) 2015 IdeaBag. All rights reserved.
//

#import "SettingsNavigationViewController.h"

@interface SettingsNavigationViewController ()

@end

@implementation SettingsNavigationViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    
    [[UINavigationBar appearance]setTintColor:[UIColor blueColor]];
    [[UINavigationBar appearance]setBarTintColor:[UIColor redColor]];
    
    
}

- (void)viewWillAppear:(BOOL)animated
{
    [super viewWillAppear:animated];
    // set navigation bar's tint color when being shown
    self.navigationController.navigationBar.barTintColor = [UIColor greenColor];
}

- (void)awakeFromNib {
    UINavigationBar *bar = [self.navigationController navigationBar];
    [bar setTintColor:[UIColor greenColor]];
    [bar setBarTintColor:[UIColor blueColor]];
    
    
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
